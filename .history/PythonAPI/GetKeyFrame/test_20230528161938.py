import cv2
import sys

img_path='./dataset'
video_path='E:/develop/yolov7/yolov7/labeling/video/1.mp4'


vc=cv2.VideoCapture('C:/Users/feng_/Desktop/COLORZ/yolov7/labeling/video/%d.mp4'%(vid_idx))

def main():

    if len(sys.argv)<3:
        print('pass 2 arguments: <videoPath> <assetsPath>')
    print(sys.argv[1])
    print(sys.argv[2])

    exit(1)


if __name__ == '__main__':
    main()
