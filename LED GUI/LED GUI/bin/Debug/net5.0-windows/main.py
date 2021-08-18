# -*- coding: utf-8 -*-
from PIL import ImageGrab
import pyautogui
import time
import serial
import sys

def dis_aver(w_kara, w_made, h_kara, h_made):
    count=red=blue=green = 0
    px = ImageGrab.grab().load()
    for y in range(int(h_kara), int(h_made), 8):
        for x in range(int(w_kara), int(w_made), 8):
            color = px[x,y]
            #print(color)
            count = count + 1
            red+=color[0]
            green+=color[1]
            blue+=color[2]

    color=[red, green, blue]
    

    for x in range (0, 3, 1):
        color[x]= int(color[x]/count)
        #print(color[x])

    Trans=""
    for i in range (0, 3, 1):
        if(color[i]<10):
            Trans=Trans+"00"+str(int(color[i]))
        elif(color[i]>9 and color[i]<100):
            Trans=Trans+"0"+str(int(color[i]))
        else:
            Trans=Trans+str(int(color[i]))
    
    #print(color)
    return Trans

def trans(rgb, direction, ser):
    Trans = direction
    for i in range (0, 3, 1):
        if(rgb[i] <10):
            Trans= Trans+"00"+ str(int(rgb[i]))
        elif(rgb[i]>9 and rgb[i] < 100):
            Trans= Trans+"0"+ str(int(rgb[i]))
        else:
            Trans= Trans+ str(int(rgb[i]))
    Trans=Trans+'\n'
    Trans=Trans.encode('utf-8')
    ser.write(Trans)
    return

def quadrant(width, height, direction):
    if(direction == 'L'):
        return(dis_aver(0, width/2, height/2, height))
    elif(direction == 'R'):
        return(dis_aver(width/2, width, height/2, height))
    elif(direction == 'Q'):
        return(dis_aver(0, width/2, 0, height/2))
    elif(direction == 'W'):
        return(dis_aver(width/2, width, 0, height/2))
    return

def half(width, height, direction):
    if(direction == 'L'):
        return(dis_aver(0, width/2, 0, height))
    elif(direction == 'R'):
        return(dis_aver(width/2, width, 0, height))

    return

def mood(ser):
    red = int(input("red value? : "))
    green = int(input("green value? : "))
    blue = int(input("blue value? : "))
    color = [red, green, blue]
    trans(color, "L", ser)
    trans(color, "R", ser)
    #trans(color, "Q", ser)
    #trans(color, "W", ser)
    return

def main():

    path = 'COM8'
    
    ser = serial.Serial(path, 9600)
    time.sleep(2)
    print("연결된 포트:", ser.portstr)

    size = pyautogui.size()
    width = size[0]
    height = size[1]
    
    mode = int(input("mode select 1:live 2:mood ? "))

    while True:
        try:
            if(mode == 1):
                half(width, height, ser)
                #quadrant(width, height, ser)
            elif(mode == 2):
                mood(ser)
            else:
                mode = int(input("mode select 1:live 2:mood ? "))
                
            #time.sleep(1)
        except:
            print('Invalid input')
            state = int(input("1:go 2:break ?: "))
            if(state == 1):
                ser.close()
                ser = serial.Serial(path, 9600)
                time.sleep(2)
                print("연결된 포트:", ser.portstr)
                mode = int(input("mode select 1:live 2:mood ? "))
                continue
            elif(state == 2):
                break
        
    ser.close()


width = sys.argv[1]
height = sys.argv[2]
direction = sys.argv[3]
width =int(width)
height = int(height)
#main()
print(quadrant(width, height, direction))
#print(half(width, height, direction))
