import cv2
import sys

assets_path="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/GetKeyFrame/dataset/assets"
video_path='C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/GetKeyFrame/dataset/test.mp4'



def main():

    # if len(sys.argv)<3:
    #     print('pass 2 arguments: <videoPath> <assetsPath>')
    #     exit(-1)
    # print("videoPath:",sys.argv[1])
    # print("assetsPath:",sys.argv[2])

    # video_path=sys.argv[1]
    # assets_path=sys.argv[2]







    vc=cv2.VideoCapture(video_path)


    fps=vc.get(cv2.CAP_PROP_FPS)

    print(fps)



    time=0

    success,frame=vc.read()

    

    idx=0
    pre_frame=[]
    while(success):
        success,frame=vc.read()
        idx+=1
        if(idx%30==0):
            dif_frame=frame-pre_frame
            gray_frame=[]
            cv2.cvtColor(src=frame,dst=gray_frame,cv2.CV_BGR2GRAY)









            pre_frame=frame



    


    


if __name__ == '__main__':
    main()
