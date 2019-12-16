#ifndef __IMAGE_H
#define __IMAGE_H

#include "stm32f10x.h"
#include "./ov7725/bsp_ov7725.h"
#include "W5500_conf.h"
#include "socket.h"
#include "stdio.h"
#include "w5500.h"
#include  <os.h>

#define PictureMaxSize	3


struct PictureQueue;
typedef uint8_t *data;
typedef struct PictureQueue *Queue;


extern uint8  remote_ip[4];											/*远端IP地址*/
extern uint16 remote_port;
extern uint8_t picture_data[3][1280];
extern Queue Q;
extern OS_MEM picture_mem;

/*图片数据队列*/
struct PictureQueue
{
	uint8_t Fornt;
	uint8_t Rear;
	uint8_t size;
};

/*初始化队列*/
void InitQueue(Queue Q);
/*判断队列是否为满*/
uint8_t IsFullQ(Queue Q);
/*判断队列是否为空*/
uint8_t IsEmpty(Queue Q);
/*入队*/
uint8 EnQueue(Queue Q);
/*出队*/
uint8 DeQueue(Queue Q);


void SendImageToComputer(uint16_t width, uint16_t height);
#endif


