from ppasr.predict import PPASRPredictor
import sys
import json



audioPath="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/SpeechRecognize/dataset/voice.wav"
textPath="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/SpeechRecognize/dataset"

DATA={}
def main():

    if len(sys.argv)<3:
        print('pass 2 arguments: <audioPath> <textPath>')
        exit(-1)
    


    predictor = PPASRPredictor(model_tag='conformer_streaming_fbank_wenetspeech')
    wav_path = 'dataset/voice.wav'
    result = predictor.predict_long(audio_data=wav_path, use_pun=True)
    score, text = result['score'], result['text']
    print(f"识别结果: {text}, 得分: {score}")


    with open('DATA.json', 'w') as file_obj:
        json.dump(DATA,file_obj)


if __name__ == '__main__':
    main()
