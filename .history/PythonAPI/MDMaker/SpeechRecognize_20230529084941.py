from ppasr.predict import PPASRPredictor
import sys
import json



# audioPath="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/SpeechRecognize/dataset/voice.wav"
# textPath="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/SpeechRecognize/dataset/DATA.json"

DATA=[]
def speechRecognize(audioPath):

    # if len(sys.argv)<3:
    #     print('pass 2 arguments: <audioPath> <textPath>')
    #     exit(-1)
    

    print("where")
    predictor = PPASRPredictor(model_tag='conformer_streaming_fbank_wenetspeech')
    wav_path = audioPath


    print(wav_path)
    # result = predictor.predict_long(audio_data=wav_path, use_pun=False)
    # score, text = result['score'], result['text']
    # print(f"识别结果: {text}, 得分: {score}")

    
    res = predictor.predict_long(audio_data=wav_path, use_pun=False)
    
    pre_time=0.0

    t_s=0
    t_e=0




    sentence=""
    for (t_start,t_end,text) in res:
        # print(t_start)

        # print(t_end)
        if(float(t_start)-float(pre_time)<1):
            if(sentence==""):
                sentence+=text
            else:
                sentence=sentence+","+text

            t_e=t_end
        else:
            sentence+="。"
            DATA.append({"t_start":t_s,"t_end":t_e,"text":sentence})

            t_s=t_start
            t_e=t_end
            sentence=text

        pre_time=t_end



    sentence+="。"
    DATA.append({"t_start":t_s,"t_end":t_e,"text":sentence})


            
            
    print(DATA)

    return DATA




    # with open(textPath, 'w') as file_obj:
    #     json.dump(DATA,file_obj)


