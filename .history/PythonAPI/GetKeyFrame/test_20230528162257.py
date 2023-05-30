import cv2
import sys

img_path='./dataset'
video_path='./dataset/test.mp4'


vc=cv2.VideoCapture(video_path)

def main():

    if len(sys.argv)<3:
        print('pass 2 arguments: <videoPath> <assetsPath>')
        exit(-1)
    print(sys.argv[1])
    print(sys.argv[2])

    


if __name__ == '__main__':
    main()
