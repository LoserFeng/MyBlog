import cv2
import sys

assets_path="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/GetKeyFrame/dataset/assets"
video_path='C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/GetKeyFrame/dataset/test.mp4'



sigma=5

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

    print("fps:",fps)

    print("image_width",image_width)
    print("image_height",image_height)
    
    mat1=cv2.getGaussianKernel(image_width,0)
    mat2=cv2.getGaussianKernel(image_height,0)
    mat1=mat1*image_width
    mat2=mat2*image_height
    mat=mat1*mat2.transpose()

    print(mat)

    cv2.imshow("test",mat)

    cv2.waitKey(0)

    return
    time=0

    success,frame=vc.read()

    

    idx=0
    pre_gray=cv2.cvtColor(frame,cv2.COLOR_BGR2GRAY)

    pre_frame=frame

    
    while(success):
        success,frame=vc.read()
        
        if(idx%30==0):
            
            
            gray_frame=cv2.cvtColor(frame,cv2.COLOR_BGR2GRAY)

            diffGray=cv2.absdiff(pre_gray, gray_frame)
            cv2.imshow('test',diffGray)


            cv2.threshold(diffGray, 10, 255.0, cv2.THRESH_BINARY)


            diffGray









            pre_gray=gray_frame

            pre_frame=frame


        idx+=1


        cv2.waitKey(10)



    


    


if __name__ == '__main__':
    main()
