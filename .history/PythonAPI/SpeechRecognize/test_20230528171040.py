from ppasr.predict import PPASRPredictor
import sys



def main():

    if(sysargs.length<3):
    predictor = PPASRPredictor(model_tag='conformer_streaming_fbank_wenetspeech')
    wav_path = 'dataset/voice.wav'
    result = predictor.predict_long(audio_data=wav_path, use_pun=True)
    score, text = result['score'], result['text']
    print(f"识别结果: {text}, 得分: {score}")





if __name__ == '__main__':
    main()
