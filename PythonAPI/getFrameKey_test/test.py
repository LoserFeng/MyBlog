import cv2
import sys
import numpy as np
import json

import matplotlib.pyplot as plt

assets_path="C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/GetKeyFrame/dataset/assets"
video_path='C:/Users/feng_/Desktop/课件/多媒体/大作业/MyBlog/PythonAPI/GetKeyFrame/dataset/test2.mp4'


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




    #cv2.namedWindow('test',cv2.WINDOW_GUI_NORMAL)


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

    

    #cv2.imshow("test",mat)


    


    print(mat)

    print(len(mat))

    #cv2.waitKey(0)
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
            #cv2.imshow("test",frame)
            
            gray_frame=cv2.cvtColor(frame,cv2.COLOR_BGR2GRAY)

            diffGray=cv2.absdiff(pre_gray, gray_frame)
            #cv2.imshow('test',diffGray)


            cv2.threshold(diffGray, 10, 255.0, cv2.THRESH_BINARY)


            value=diffGray*mat
            #cv2.imshow('test',frame)

            res=np.sum(value)


            print(res)

            

            
            if(res>=10 and len(arr)>0 and arr[-1]<10):
                
                #cv2.imwrite(assets_path+str(num)+".jpg",pre_frame)

                save_image(assets_path+"/"+str(num)+".jpg",pre_frame)
                
                
                DATA[idx/fps]=str(num)+".jpg"
                num+=1


            arr.append(res)
            pre_gray=gray_frame

            pre_frame=frame


        idx+=1

        success,frame=vc.read()

       # cv2.waitKey(10)


    



    total_width, n = 0.8, 3
    # 每种类型的柱状图宽度
    width = total_width / n

    # 重新设置x轴的坐标
    x=range(0,len(arr))



    plt.xlabel("帧序号")
    plt.ylabel("帧差")


    # 画柱状图
    plt.bar(x, arr, width=width, label="test")

    # 显示图例
    plt.legend()
    # 显示柱状图

    plt.show()





    print(arr)

    with open(DATA_path, 'w') as file_obj:
        json.dump(DATA,file_obj)



    

def save_image(img_path,img):
    cv2.imencode('.png', img)[1].tofile(img_path)
    


if __name__ == '__main__':
    main()
