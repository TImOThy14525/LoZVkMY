// 代码生成时间: 2025-10-02 02:26:28
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.UI.Xaml.Controls;

namespace BlazorApp
{
    // 语音识别组件
    public partial class SpeechRecognitionSystem : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        // 语音识别器
        private SpeechRecognizer speechRecognizer;

        // 识别结果
        private string recognitionResult;

        protected override async Task OnInitializedAsync()
        {
            // 初始化语音识别器
            speechRecognizer = new SpeechRecognizer(SpeechRecognizerSystemProfile.Dictation);

            // 注册语音识别事件
            speechRecognizer.RecognitionQualityDegrading += SpeechRecognizer_RecognitionQualityDegrading;
            speechRecognizer.StateChanged += SpeechRecognizer_StateChanged;
            speechRecognizer.HypothesisGenerated += SpeechRecognizer_HypothesisGenerated;
            speechRecognizer.ResultGenerated += SpeechRecognizer_ResultGenerated;

            // 启动语音识别器
            await speechRecognizer.CompileConstraintsAsync();
        }

        private void SpeechRecognizer_HypothesisGenerated(SpeechRecognizer sender, SpeechRecognitionHypothesisGeneratedEventArgs args)
        {
            // 在用户说话时生成临时识别结果
            recognitionResult = args.Hypothesis.Text;
            StateHasChanged();
        }

        private void SpeechRecognizer_ResultGenerated(SpeechRecognizer sender, SpeechRecognitionResultGeneratedEventArgs args)
        {
            // 当识别结果稳定时更新识别结果
            recognitionResult = args.Result.Text;
            StateHasChanged();
        }

        private void SpeechRecognizer_StateChanged(SpeechRecognizer sender, SpeechRecognizerStateChangedEventArgs args)
        {
            // 处理语音识别器状态变化
            switch (args.State)
            {
                case SpeechRecognizerState.Idle:
                    Console.WriteLine("Speech recognizer is idle.");
                    break;
                case SpeechRecognizerState.Capturing:
                    Console.WriteLine("Speech recognizer is capturing.");
                    break;
                case SpeechRecognizerState.Processing:
                    Console.WriteLine("Speech recognizer is processing.");
                    break;
                case SpeechRecognizerState.SoundStarted:
                    Console.WriteLine("Speech recognizer detected sound.");
                    break;
                case SpeechRecognizerState.SoundEnded:
                    Console.WriteLine("Speech recognizer finished capturing sound.");
                    break;
            }
        }

        private void SpeechRecognizer_RecognitionQualityDegrading(SpeechRecognizer sender, SpeechRecognitionQualityDegradingEventArgs args)
        {
            // 处理语音识别质量下降
            Console.WriteLine("Speech recognition quality is degrading.");
        }

        private async Task StartSpeechRecognition()
        {
            try
            {
                // 启动语音识别
                await speechRecognizer.StartRecognitionAsync();
            }
            catch (Exception ex)
            {
                // 处理错误
                Console.WriteLine($"Error starting speech recognition: {ex.Message}");
            }
        }

        private async Task StopSpeechRecognition()
        {
            try
            {
                // 停止语音识别
                await speechRecognizer.StopRecognitionAsync();
            }
            catch (Exception ex)
            {
                // 处理错误
                Console.WriteLine($"Error stopping speech recognition: {ex.Message}");
            }
        }

        // 组件生命周期方法
        public void Dispose()
        {
            // 释放语音识别器资源
            speechRecognizer.Dispose();
        }

        // 组件模板
        private string Template = "@code { <button @onclick="StartSpeechRecognition">Start Recognition</button> <p>Recognition Result: @recognitionResult</p> }";
    }
}
