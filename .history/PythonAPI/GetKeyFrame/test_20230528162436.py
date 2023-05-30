import cv2
import sys

assets_path="./dataset/assets"
video_path='./dataset/test.mp4'



def main():

    if len(sys.argv)<3:
        print('pass 2 arguments: <videoPath> <assetsPath>')
        exit(-1)
    print("videoPath{%d}",sys.argv[1])
    print("assetsPath{%d}",sys.argv[2])
    



    vc=cv2.VideoCapture(video_path)


    


if __name__ == '__main__':
    main()
