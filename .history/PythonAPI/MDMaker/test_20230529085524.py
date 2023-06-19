import SpeechRecognize

import GetKeyFrame

import sys

video_path="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/TEST.TEST/Resource/tmp/test.mp4"
audio_path="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/TEST.TEST/Resource/tmp/voice.wav"
assets_path="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/TEST.TEST/Resource/assets"
tmp_path="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/TEST.TEST/Resource/tmp"


def main():


    # if len(sys.argv)<5:
    #     print('pass 4 arguments: <videoPath> <audioPath>  <assetsPath> <tmpPath>')
    #     exit(-1)

    # video_path=sys.argv[1]
    # audio_path=sys.argv[2]
    # assets_path=sys.argv[3]
    # tmp_path=sys.argv[4]

    

    
    print("进行关键帧提取。。。")
    frame_data=GetKeyFrame.getKeyFrame(video_path,assets_path)



    print("关键帧提取完成")


    print("进行语音识别。。。")

    text_data=SpeechRecognize.speechRecognize(audio_path)



    fileName='blog.md'
    with open(assets_path+fileName,'w',encoding='utf-8')as file:
        file.write('average')



    
    






    # SpeechRecognize.speechRecognize()

    # print("begin to merge json")
    

    
if __name__ =="__main__":
    main()