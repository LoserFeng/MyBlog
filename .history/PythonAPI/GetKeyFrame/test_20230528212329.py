import cv2
import sys
import numpy as np
import json

assets_path="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/GetKeyFrame/dataset/assets"
video_path='C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/GetKeyFrame/dataset/test.mp4'


DATA_path="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/GetKeyFrame/dataset/DATA.json"


sigma=0

def main():

    # if len(sys.argv)<3:
    #     print('pass 2 arguments: <videoPath> <assetsPath>')
    #     exit(-1)
    # print("videoPath:",sys.argv[1])
    # print("assetsPath:",sys.argv[2])

    # video_path=sys.argv[1]
    # assets_path=sys.argv[2]

    DATA={}




    cv2.namedWindow('test',cv2.WINDOW_GUI_NORMAL)


    vc=cv2.VideoCapture(video_path)


    fps=int(vc.get(cv2.CAP_PROP_FPS))

    image_width=int(vc.get(cv2.CAP_PROP_FRAME_WIDTH))   #视频帧宽度
    image_height=int(vc.get(cv2.CAP_PROP_FRAME_HEIGHT))   #视频帧高度

    print("fps:",fps)

    print("image_width",image_width)
    print("image_height",image_height)
    
    mat1=cv2.getGaussianKernel(image_width,sigma)
    mat2=cv2.getGaussianKernel(image_height,sigma)
    mat1=mat1
    mat2=mat2
    mat=mat1.transpose()*mat2

    

    print(mat)

    print(len(mat))


    time=0

    success,frame=vc.read()

    

    idx=0
    pre_gray=cv2.cvtColor(frame,cv2.COLOR_BGR2GRAY)

    pre_frame=frame

    print(len(frame))

    arr=[]
    num=0

    
    while(success):
        
        
        if(idx%fps==0):
            
            
            gray_frame=cv2.cvtColor(frame,cv2.COLOR_BGR2GRAY)

            diffGray=cv2.absdiff(pre_gray, gray_frame)
            #cv2.imshow('test',diffGray)


            cv2.threshold(diffGray, 10, 255.0, cv2.THRESH_BINARY)


            value=diffGray*mat
            #cv2.imshow('test',frame)

            res=np.sum(value)


            print(res)

            

            
            if(res>=10 and len(arr)>0 and arr[-1]<10):
                print("where")
                cv2.imwrite(assets_path+"/"+str(num)+".jpg",pre_frame)
                num+=1
                
                DATA[idx/fps]=str(num)+".jpg"


            arr.append(res)
            pre_gray=gray_frame

            pre_frame=frame


        idx+=1

        success,frame=vc.read()


        




    print(arr)

    with open(DATA_path, 'w') as file_obj:
        json.dump(DATA,file_obj)



    


    


if __name__ == '__main__':
    main()
