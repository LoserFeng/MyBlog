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




    cv2.namedWindow('test',cv2.WINDOW_GUI_NORMAL)


    vc=cv2.VideoCapture(video_path)


    fps=vc.get(cv2.CAP_PROP_FPS)

    image_width=int(vc.get(cv2.CAP_PROP_FRAME_WIDTH))   #视频帧宽度
    image_height=int(vc.get(cv2.CAP_PROP_FRAME_HEIGHT))   #视频帧高度

    print(fps)

    print(image_width)
    print(image_height)



    time=0

    success,frame=vc.read()

    

    idx=0
    pre_frame=cv2.cvtColor(frame,cv2.COLOR_BGR2GRAY)
    while(success):
        success,frame=vc.read()
        
        if(idx%30==0):
            
            
            gray_frame=cv2.cvtColor(frame,cv2.COLOR_BGR2GRAY)

            diffGray=cv2.absdiff(pre_frame, gray_frame)
            cv2.imshow('test',diffGray)


            cv2.threshold(diffGray, 10, 255.0, cv2.THRESH_BINARY)









            pre_frame=gray_frame


        idx+=1


        cv2.waitKey(10)



    


    


if __name__ == '__main__':
    main()
