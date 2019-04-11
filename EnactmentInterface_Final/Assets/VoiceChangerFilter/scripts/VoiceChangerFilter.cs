using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;

public class VoiceChangerFilter : MonoBehaviour {

    [Range(0, 5)]
    public float _formant;
    [Range(0, 3)]
    public float _pitch;

    [DllImport("world_dll")]
    private static extern int filter(double[] datas, int data_size, int fs, float pitch, float formant);

    [DllImport("world_dll")]
    private static extern int filter_debug(double[] datas, int data_size, int fs, float pitch, float formant, int[] time);

    int outputSampleRate = 48000;

    void Awake()
    {
        outputSampleRate = AudioSettings.outputSampleRate;
    }

    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && audioSource.clip != null)
        {
            outputSampleRate = audioSource.clip.frequency;
        }

        _buffer = new double[_bufferNum][];
        for (int i = 0; i < _bufferNum; i++)
        {
            _buffer[i] = new double[(int)(outputSampleRate * _bufferLimitTime)];
        }
        _bufferUseSize = new int[_bufferNum];

        _started = true;
    }

    double[][] _buffer;
    int[] _bufferUseSize;
    int _bufferingSeek = 0;
    int _playingSeek = 0;
    int _bufferingIndex = 0;
    int _playingIndex = 0;
    int _bufferNum = 4;
    float _bufferTime = 0.3f;           //Change Buffer Time
    float _bufferLimitTime = 0.4f;  //All Buffer Time
    bool _started = false;

    void copyBuffer(double[] src_buffer, double[] dst_buffer, int src_startIndex, int dst_startIndex, int size)
    {
        //if this case happened. mean buffer over run
        if (src_buffer.Length < src_startIndex + size || dst_buffer.Length < dst_startIndex + size)
        {
            Debug.Log("error");
        }

        for (int i = 0; i < size; i++)
        {
            dst_buffer[dst_startIndex + i] = src_buffer[src_startIndex + i];
        }
    }

    void memBuffer(double[] dst_buffer, int dst_startIndex, double value, int size)
    {
        for (int i = 0; i < size; i++)
        {
            dst_buffer[dst_startIndex + i] = value;
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (data.Length == 0)
            return;

        if (!_started)
            return;

        //monoral
        if (channels == 1)
        {
            double[] monoral_data = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                monoral_data[i] = data[i];
            }

            OnProcess(monoral_data);

            for (int i = 0; i < data.Length; i++)
            {
                monoral_data[i] = (float)data[i];
            }
        }
        //stereo or more channels
        else
        {
            int monoralDataLength = data.Length / channels;
            //convert stereo sound to monoral
            double[] monoral_data = new double[monoralDataLength];

            for (int i = 0; i < monoralDataLength; i++)
            {
                monoral_data[i] = data[i * channels];
            }

            OnProcess(monoral_data);
            

            //fill monoral data to channels
            for (int i = 0; i < data.Length; i+=channels)
            {
                int j = (int)(i / channels);
                for (int k = 0; k < channels; k++)
                {
                    data[i+k] = (float)monoral_data[j];
                }
            }

        }
    }


    void StartThread(double[] data, int data_size, int fs, float pitch, float formant)
    {
        FilterParam param = new FilterParam();
        param.data = data;
        param.data_size = data_size; 
        param.fs = fs;
        param.pitch = pitch;
        param.formant = formant; 
        Thread thread = new Thread( new ParameterizedThreadStart(ThreadWork));
        thread.Start(param);
    }

    void ThreadWork(object _param)
    {
        FilterParam param = (FilterParam)_param;
        filter(param.data, param.data_size, param.fs, param.pitch, param.formant);
    }

    void OnProcess(double[] monoral_data)
    {

        //process charge buffer
        if (_bufferingSeek < _bufferTime * outputSampleRate)
        {
            copyBuffer(monoral_data, _buffer[_bufferingIndex], 0, _bufferingSeek, monoral_data.Length);
            _bufferingSeek += monoral_data.Length;
            _bufferUseSize[_bufferingIndex] = _bufferingSeek;
        }
        else //change to next buffer
        {


            StartThread(_buffer[_bufferingIndex], _bufferUseSize[_bufferingIndex], outputSampleRate, Mathf.Max( _pitch, 0.3f), _formant);

            _bufferingIndex = (_bufferingIndex + 1) % _bufferNum;
            _bufferingSeek = 0;
            copyBuffer(monoral_data, _buffer[_bufferingIndex], 0, _bufferingSeek, monoral_data.Length);
            _bufferingSeek += monoral_data.Length;
            _bufferUseSize[_bufferingIndex] = _bufferingSeek;
        }

        //wait for charge buffer
        if ((_playingIndex + 2) % _bufferNum == _bufferingIndex)
        {

            //process play sound
            if (_playingSeek + monoral_data.Length < _bufferUseSize[_playingIndex])
            {
                copyBuffer(_buffer[_playingIndex], monoral_data, _playingSeek, 0, monoral_data.Length);
                _playingSeek += monoral_data.Length;
            }
            else //change to next buffer
            {
                int copyBufferSize = _bufferUseSize[_playingIndex] - _playingSeek;
                copyBuffer(_buffer[_playingIndex], monoral_data, _playingSeek, 0, copyBufferSize);   //残りのバッファ全コピ
                _playingIndex = (_playingIndex + 1) % _bufferNum;   //次のバッファへ
                _playingSeek = 0;

                int nextCopyBufferSize = monoral_data.Length - copyBufferSize;
                copyBuffer(_buffer[_playingIndex], monoral_data, _playingSeek, copyBufferSize, nextCopyBufferSize);
                _playingSeek += nextCopyBufferSize;
            }
        }
        else
        {

            memBuffer(monoral_data, 0, 0f, monoral_data.Length);
        }

    }
}

//for thread
public class FilterParam
{
    public double[] data;
    public int data_size;
    public int fs;
    public float pitch;
    public float formant;
}
