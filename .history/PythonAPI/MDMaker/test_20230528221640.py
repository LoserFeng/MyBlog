import SpeechRecognize

import GetKeyFrame

import sys


def main():


    if len(sys.argv)<5:
        print('pass 4 arguments: <videoPath> <audioPath>  <assetsPath> <tmpPath>')
        exit(-1)

    video_path=sys.argv[1]
    audio_path=sys.argv[2]
    assets_path=sys.argv[3]
    tmp_path=sys.argv[4]
    
    

    

    GetKeyFrame.getKeyFrame(video_path,)




    # SpeechRecognize.speechRecognize()

    # print("begin to merge json")
    

    
if __name__ =="__main__":
    main()