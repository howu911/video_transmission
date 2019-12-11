/*
 * 超声波避障接口为D6,D7/P9端口。
 * PS2遥控接口为D10，D11,D12,D13/P14端口/P7端口供电。
 * 2015-02-01
 * hellorobot 懒猫侠 
 * 博客 http://hellorobot.blog.163.com
 * 网店 http://hellorobot.taobao.com 
 */

/////////////////*****************不可修改***************************
#include "PS2X_lib.h"                                          // 导入 Psx 库文件
#include <SoftwareSerial.h> 
#include <SR04.h>
Psx Psx;                                                  // Initializes the library
#define dataPin  10        
#define cmndPin   11        
#define attPin    12         
#define clockPin   13   
//#define center  0x7F    /////////定义摇杆中间值
#define TRIG_PIN 6
#define ECHO_PIN 7

////////*********************按键对应动作组指令**************************
String  START_DATA= "RUN001C01S50";  
String  SELECT_DATA= "RUN002C01S50";  
String  PAD_UP_DATA= "RUN003C01S50";   
String  PAD_DOWN_DATA= "RUN004C01S50";  
String  PAD_LEFT_DATA= "RUN005C01S50";
String  PAD_RIGHT_DATA= "RUN006C01S50";
String  TRIANGLE_DATA= "RUN009C01S50";  //三角
String  CIRCLE_DATA= "RUN010C01S50";  //圈圈
String  CROSS_DATA= "RUN015C01S50";  //叉叉
String  SQUARE_DATA= "RUN021C01S50";  //方框
String  L1_DATA= "RUN017C01S50";
String  R1_DATA= "RUN018C01S50";
String  L2_DATA= "RUN001C01S50";
String  R2_DATA= "RUN001C01S50";
String  L3_DATA= "RUN001C01S50";
String  R3_DATA= "RUN001C01S50";
String  LXmin_DATA= "RUN005C01S50";
String  LXmax_DATA= "RUN006C01S50";
String  LYmin_DATA= "RUN003C01S50";
String  LYmax_DATA= "RUN004C01S50";
String  RXmin_DATA= "RUN013C01S50";
String  RXmax_DATA= "RUN014C01S50";
String  RYmin_DATA= "RUN001C01S50";
String  RYmax_DATA= "RUN001C01S50";

byte START=0;
byte SELECT=0;
byte PAD_UP=0;
byte PAD_DOWN=0;
byte PAD_LEFT=0;
byte PAD_RIGHT=0;
byte TRIANGLE=0;
byte CIRCLE=0;
byte CROSS=0;
byte SQUARE=0;
byte PAD_L1=0;
byte PAD_L2=0;
byte PAD_L3=0;
byte PAD_R1=0;
byte PAD_R2=0;
byte PAD_R3=0;

byte LYmin=0;
byte LYmax=0;
byte LXmin=0;
byte LXmax=0;
byte RYmin=0;
byte RYmax=0;
byte RXmin=0;
byte RXmax=0;
byte Flag_Joy=0;

long count = 0;

int JoyL=0;        //摇杆左
int JoyR=0;        // 摇杆右
///定义摇杆模拟量
  int X1;
  int Y1;
  int X2;
  int Y2;
  int str=0;//START按键次数定义

SoftwareSerial mySerial(8, 9); // RX, TX  
int val;

SR04 sr04 = SR04(ECHO_PIN,TRIG_PIN);
SR04 sr04D = SR04(ECHO_PIN,TRIG_PIN);
long m;
byte sr = 0;
byte BN_F = 0;

int BTON = 2;

int LEDA = 4;
int LEDB = 5;

byte start_flag = 0;
////////////////////////////
void setup()
{
  Psx.setupPins(dataPin, cmndPin, attPin, clockPin);   // 设置接收器对应ARDUINO 端口   
delay(100); 
   
   Psx.Config_Mode(psxAnalog); 
    Psx.initcontroller(psxAnalog);
  Serial.begin(115200); ///设置波特率
  mySerial.begin(9600);
        tone(A3,200); 
        delay(100);    
   pinMode(A3,INPUT);
    delay(100);
      tone(A3,500); 
        delay(100);
    pinMode(A3,INPUT);
    delay(100);
      tone(A3,800); 
        delay(100);
  pinMode(A3,INPUT);
  delay(2000);
  Serial.println("startOK"); 
    pinMode(LEDA,INPUT);
  pinMode(LEDB,INPUT);
   pinMode(BTON,OUTPUT);
 digitalWrite(BTON,HIGH);
 digitalWrite(LEDA,HIGH); 
 digitalWrite(LEDB,HIGH);

}

void loop()
{
  if(Serial.available()>0)
  {
    char ch = char(Serial.read());
    //Serial.println(ch);
    if(ch == 's')
    {
      Serial.println(START_DATA);
      delay(100); 
      Serial.println(START_DATA);
    }
    if(ch == 'u')
    {
      Serial.println(PAD_UP_DATA);
      delay(1000); 
    }
    if(ch == 'd')
    {
      Serial.println(PAD_DOWN_DATA);
      delay(1000); 
    }
    if(ch == 'l')
    {
      Serial.println(PAD_LEFT_DATA);
      delay(1000); 
    }
    if(ch == 'r')
    {
      Serial.println(PAD_RIGHT_DATA);
      delay(1000); 
    }    
  }

 ///////////////////////////////////////////////    
   if(digitalRead(LEDA)==LOW)
  {
    delay(10);
    if(digitalRead(LEDA)==LOW)
    {
    Serial.println("RUN005C01S50"); 
    //delay(100);
    }
  }
  
  if(digitalRead(LEDB)==LOW)
  {
    delay(10);
    if(digitalRead(LEDB)==LOW)
    {
    Serial.println("RUN006C01S50"); 
    //delay(100);
    }
  }
 ////////////////////////////////////////////////// 


            

 
    if(mySerial.available()){
    val = mySerial.read();
   int b = mySerial.peek();
    if(val == '#'){
 switch(b){                     //判定按下的是哪个按键
    case 'A': Serial.println("RUN001C01S50");delay(150);break;
    case 'B': Serial.println("RUN001C01S50");delay(150);break;
    case 'C': Serial.println("RUN001C01S50");delay(150);break;
    case 'D': Serial.println("RUN001C01S50");delay(150);break;
    case 'E': Serial.println("RUN001C01S50");delay(150);break;
    case 'F': Serial.println("RUN001C01S50");delay(150);break;
    case 'G': Serial.println("RUN001C01S50");delay(150);break;
    case 'H': Serial.println("RUN001C01S50");delay(150);break;
    case 'I': Serial.println("RUN001C01S50");delay(150);break;
    case 'J': Serial.println("RUN001C01S50");delay(150);break;
    case 'K': Serial.println("RUN001C01S50");delay(150);break;
    case 'L': Serial.println("RUN001C01S50");delay(150);break;
    case 'M': Serial.println("RUN001C01S50");delay(150);break;
    case 'N': Serial.println("RUN001C01S50");delay(150);break;
    case 'O': Serial.println("RUN001C01S50");delay(150);break;
    case 'P': Serial.println("RUN001C01S50");delay(150);break;
    case 'Q': Serial.println("RUN001C01S50");delay(150);break;
    case 'R': Serial.println("RUN001C01S50");delay(150);break;
    case 'S': 
          sr = 1;
          delay(10);
          sr = 1;
    Serial.println("RUN001C01S50");
    delay(150);
    break;
    case 'T': 
          sr = 0;
          delay(10);
          sr = 0;
    Serial.println("RUN001C01S50");
    delay(150);
    break;
    case 'U': Serial.println("RUN001C01S50");delay(150);break;
    case 'V': Serial.println("RUN001C01S50");delay(150);break;
    case 'W': Serial.println("RUN001C01S50");delay(150);break;
    case 'X': Serial.println("RUN001C01S50");delay(150);break;
    case 'Y': Serial.println("RUN001C01S50");delay(150);break;
    case 'Z': Serial.println("RUN001C01S50");delay(150);break;
  default:
  break;
  }
    }
} 

  Psx.poll();    delay(50);           // 刷新PSX数据   
      
    //////////////////////Start 
  if (Psx.digital_buttons == psxStrt && START==0 )        ///按下START                               
  {  
      START=1; 
      Serial.println("STOP");
      delay(100);     
      Serial.println(START_DATA);
      delay(100); 
      Serial.println(START_DATA);
      str=0;
  }

    if (Psx.digital_buttons != psxStrt && START==1)    //////////松开START
    {
      START=0;
    //Serial.println("#1G1CS128"); //发送一次 released_START
    }

    //////////////////////Select 

    if (Psx.digital_buttons == psxSlct&& SELECT==0)        //按下SELECT                              
  {  
      SELECT=1;      
      Serial.println(SELECT_DATA);
      str=0;
  }
    if (Psx.digital_buttons != psxSlct&& SELECT==1)    //////////SELECT
    {
      SELECT=0;
    //Serial.println("#1G1CS128"); //发送一次 SELECT
    }
  
    //////////////////////UP 

       if (Psx.digital_buttons == psxUp&& PAD_UP==0)                                      
  {  
      Serial.println(PAD_UP_DATA);
      PAD_UP=1;
     
     str=0;
    }
    if (Psx.digital_buttons != psxUp&& PAD_UP==1)    //////////PAD_UP
    {
      PAD_UP=0;
    //Serial.println("#1G1CS128"); //发送一次 PAD_UP
    }

   //////////////////////DOWN
    if (Psx.digital_buttons == psxDown&& PAD_DOWN==0 )                              
  {  
     Serial.println(PAD_DOWN_DATA);
     PAD_DOWN=1;
    
    
   str=0;
    }
    if (Psx.digital_buttons != psxDown&& PAD_DOWN==1)    //////////PAD_DOWN
    {
      PAD_DOWN=0;
    //Serial.println("#1G1CS128"); //发送一次 PAD_DOWN
    }
    
  /////////////////////LEFT

        if (Psx.digital_buttons == psxLeft&& PAD_LEFT==0 )                                      
  { 
   
     Serial.println(PAD_LEFT_DATA);
     PAD_LEFT=1;
     
    
    str=0;
    }
    if (Psx.digital_buttons != psxLeft&& PAD_LEFT==1)    //////////PAD_LEFT
    {
      PAD_LEFT=0;
    //Serial.println("#1G1CS128"); //发送一次 PAD_LEFT
    }

  /////////////////////RIGHT

        if (Psx.digital_buttons == psxRight&& PAD_RIGHT==0)                                      
  {    
     Serial.println(PAD_RIGHT_DATA);
     PAD_RIGHT=1;
    
    str=0;
   }
     if (Psx.digital_buttons != psxRight&& PAD_RIGHT==1)    //////////PAD_RIGHT
    {
      PAD_RIGHT=0;
    //Serial.println("#1G1CS128"); //发送一次 PAD_RIGHT
    }  


   
    ////////////////L1///////////////
 
      if (Psx.digital_buttons == psxL1&& PAD_L1==0)   ////////一直按住L1                                                                                                                                        
  {  
       Serial.println(L1_DATA); 
     PAD_L1=1;
     
       str=0;
  }
     if (Psx.digital_buttons != psxL1&& PAD_L1==1)    //////////PAD_L1
    {
      PAD_L1=0;
    //Serial.println("#1G1CS128"); //发送一次 PAD_L1
    }

  
  
  ////////////////////R1////////////////

      if (Psx.digital_buttons == psxR1&& PAD_R1==0)   ////////一直按住R1                                                                                                                                        
  {  
       Serial.println(R1_DATA); 
     PAD_R1=1;
     
       str=0;
  }
     if (Psx.digital_buttons != psxR1&& PAD_R1==1)    //////////PAD_R1
    {
      PAD_R1=0;
    //Serial.println("#1G1CS128"); //发送一次 PAD_R1
    }

  

   ////////////////L2////////////////////

      if (Psx.digital_buttons == psxL2)                                                                                                                                           
  {  
      Serial.println("Line#1P452T500");// 
        //Serial.println(L2_DATA);
     delay(100);
     str=0;
   
  }


  
  ///////////////////////R2  /////////////////////////

      if (Psx.digital_buttons == psxR2)     /////////一直按住R2                                 
  {  
 

           
            Serial.println("Line#1P212T500");//
       
        //Serial.println(R2_DATA);
     delay(100);
    str=0;
 
  }

    

    /////////////////////TRIANGLE   三角
                                  
    

      if (Psx.digital_buttons == psxTri)  /////一直按住三角                                 
  {  
        Serial.println(TRIANGLE_DATA);
     delay(100);
    str=0;
  }

  /////////////////////CIRCLE   圈圈
 
     if (Psx.digital_buttons == psxO)        ///一直按住圈圈                              
  {  
     Serial.println(CIRCLE_DATA);
     delay(100);
     str=0;    
   
  } 
    /////////////////////CROSS   叉叉

    if (Psx.digital_buttons == psxX )        //一直按住叉叉                              
  {  
    
    Serial.println(CROSS_DATA);
     
     delay(100);
     str=0;
  }

  /////////////////////SQUARE   方框

      if (Psx.digital_buttons == psxSqu )     ////按住方框                                
  {  
    
    Serial.println(SQUARE_DATA);
     delay(100);
     str=0;
  }
  
      if (Psx.digital_buttons == psxJoyL )     ////                                
  {  

      sr = 1;
      delay(20);
      sr = 1; 
  }
      if (Psx.digital_buttons == psxJoyR )     ////                                
  {  

      sr = 0;
      delay(20);
      sr = 0;
  }
    
  
      ///////////////////摇杆////////////************************************///////////
      
    if (Psx.Controller_mode==0x73) /////如果是模拟模式 才开启摇杆
   
  {   
  int x1 = Psx.Left_x;          // x= 0-128-255
  int y1 = Psx.Left_y;           //y= 0-127-255
  int x2 = Psx.Right_x;  
  int y2 = Psx.Right_y;


   
////////////////////////////
    if(y1<120&&LYmin==0)    
        {
            LYmin=1;

           
                  
            Flag_Joy=1;
        }
    
    if(y1>120&&y1<130&&LYmin==1) 
        {
            LYmin=0;
              //Serial.println(F("STOP"));
                //  delay(250);
    
            // Serial.println(START_DATA);
            delay(150);
    
            
            Flag_Joy=0;
             
        } 
    
    if(Flag_Joy==1&&LYmin==1) 
       {
         //Flag_Joy=0;
            Serial.println(LYmin_DATA);
           delay(150);
           
       }    
    
    
////////////////////////////////////////
    if(y1>135&&LYmax==0)    
        {
            LYmax=1;
            
           // delay(150);         
            Flag_Joy=1;
        }
    
    if(y1>120&&y1<135&&LYmax==1) 
        {
            LYmax=0;
       //  Serial.println(F("STOP"));
           //       delay(250);
          //Serial.println(START_DATA);
       //  delay(150);
         
                 // delay(150);
                      
            
            Flag_Joy=0;
             
        } 
    
    if(Flag_Joy==1&&LYmax==1) 
       {
         //Flag_Joy=0;
          Serial.println(LYmax_DATA);
           delay(100);
       } 
///////////////////////////////////////
    if(x1<120&&LXmin==0)    
        {
            LXmin=1;
          
             //delay(150);
           
                  
            Flag_Joy=1;
        }
    
    if(x1>120&&x1<130&&LXmin==1) 
        {
            LXmin=0;

            //Serial.println(F("STOP"));
             //     delay(250);
             //Serial.println(START_DATA);
            //delay(150);
       
            
            Flag_Joy=0;
             
        } 
    
    if(Flag_Joy==1&&LXmin==1) 
       {
         //Flag_Joy=0;
          Serial.println(LXmin_DATA);
           delay(100);
           
       }   
//////////////////////////////////////////////    
   if(x1>135&&LXmax==0)    
        {
            LXmax=1;
          
           // delay(150);         
            Flag_Joy=1;
        }
    
    if(x1>120&&x1<135&&LXmax==1) 
        {
            LXmax=0;
           // Serial.println(F("STOP"));
              //    delay(250);
          //Serial.println(START_DATA);
       //  delay(150);
 
            Flag_Joy=0;
             
        } 
    
    if(Flag_Joy==1&&LXmax==1) 
       {
         //Flag_Joy=0;
           Serial.println(LXmax_DATA);
           delay(100);
       } 


       
//////////////////////////////////////////////////
    if(y2<120&&RYmin==0)    
        {
            RYmin=1;
           
             //delay(150);
           
                  
            Flag_Joy=1;
        }
    
    if(y2>120&&y2<130&&RYmin==1) 
        {
            RYmin=0;

             //Serial.println(F("STOP"));
                 // delay(250);
                 // delay(150);       
            //Serial.println(START_DATA);
            Flag_Joy=0;
             
        } 
    
    if(Flag_Joy==1&&RYmin==1) 
       {
        // Flag_Joy=0;
           Serial.println(RYmin_DATA);
           delay(100);
           
       }    
    
 /////////////////////////////////////   
    if(y2>135&&RYmax==0)    
        {
            RYmax=1;
          
            // delay(150);         
            Flag_Joy=1;
        }
    
    if(y2>120&&y2<135&&RYmax==1) 
        {
            RYmax=0;
            //Serial.println(F("STOP"));
            //      delay(250);

                //  delay(150);
                 //Serial.println(START_DATA);     
            
            Flag_Joy=0;
             
        } 
    
    if(Flag_Joy==1&&RYmax==1) 
       {
        // Flag_Joy=0;
            Serial.println(RYmax_DATA);
           delay(150);
       }  
  /////////////////////////////  
   if(x2<120) 
    {

          Serial.println(RXmin_DATA);           //R--LEFT
          delay(100);
        
    } 
 /////////////////////////////////   
   if(x2>135) 
    {

          Serial.println(RXmax_DATA);           //R--RIGHT
          delay(100);
        
    }                                            


 }
  
  //delay(1000);
   int Bo;
  int BN;
      Bo=analogRead(A7);//传感器接到模拟口0
   if(Bo>1023)
   {
    BN=Bo/4.1192; 
     
   }
   else
   {
    BN=Bo*0.9765; 
     
   }
   
     if(BN<680)
     {
       delay(1000);
     //Serial.println("#Q"); 
     //delay(2000);
        tone(A3,1000,1500); 
        delay(2000);    
   pinMode(A3,INPUT);
   Serial.println(BN);
    Serial.println(F("STOP"));
 digitalWrite(BTON,LOW);
     }
  

}


