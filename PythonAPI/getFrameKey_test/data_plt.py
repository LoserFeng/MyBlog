
import cv2
import sys
import numpy as np
import json

import matplotlib.pyplot as plt



arr=[]

f4=open('./dataset/arr.txt', encoding='utf-8')
#此时只读取了一行
contents2=f4.readline()
print(contents2)
i=1
#利用循环全部读出
while contents2:

    

    arr.append(float(contents2))
    contents2=f4.readline()
    i+=1
    if(i==100):
        break
f4.close()


total_width  = 1
# 每种类型的柱状图宽度
width = total_width 

# 重新设置x轴的坐标
x=range(0,len(arr))



plt.xlabel("frame num")
plt.ylabel("frame diff")


# 画柱状图
plt.bar(x, arr, width=width, label="frame diff")

# 显示图例
plt.legend()
# 显示柱状图

plt.show()