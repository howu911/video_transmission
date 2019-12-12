#include "image.h"

/*初始化队列*/
void InitQueue(Queue Q)
{
	int i = 0;
	for(i = 0; i < PictureMaxSize; ++i)
	{
		Q->data[i] = NULL;
	}
	Q->Fornt = 1;
	Q->Rear = 0;
	Q->size = 0;
}

/*判断队列是否为空*/
uint8_t IsEmpty(Queue Q)
{
	return Q->size == 0;
}
/*判断队列是否为满*/
uint8_t IsFullQ(Queue Q)
{
	return Q->size >= PictureMaxSize;
}

static int Scc(int value)
{
	if (++value == PictureMaxSize)
		value = 0;
	return value;
}
/*入队*/
void EnQueue(data Node, Queue Q)
{
	if(IsFullQ(Q))
		return;
	Q->size++;
	Q->Rear = Scc(Q->Rear);
	Q->data[Q->Rear] = Node;
}
/*出队*/
data DeQueue(Queue Q)
{
	data re_val;
	if(IsEmpty(Q))
		return NULL;
	re_val = Q->data[Q->Fornt];
	Q->size--;
	Q->Fornt = Scc(Q->Fornt);
	return re_val;
}


void SendImageToComputer(uint16_t width, uint16_t height)
{
	OS_ERR  err;
//	uint8_t AckData[1] = { 0x01};
//	uint8 buff[1] = {0};

	
	uint16_t i = 0,j = 0,k = 0; 
	uint16_t data_line = 0;
	uint8_t Camera_Data;
	uint8_t data[1280] = {0};
	
	for(i = 0; i < height; i++)
	{
		for(j = 0; j < width; j++)
		{
			READ_FIFO_PIXEL(Camera_Data);		/* 从FIFO读出一个rgb565像素的高位到Camera_Data变量 */
			data[k++] = Camera_Data;
			READ_FIFO_PIXEL(Camera_Data);		/* 从FIFO读出一个rgb565像素的低位到Camera_Data变量 */
			data[k++] = Camera_Data;
		}
		data_line++;
		if(data_line%2 == 0)
		{			
			sendto(SOCK_UDPS,data, width*4, remote_ip, remote_port);
			k = 0;
		}
		//OSTimeDlyHMSM ( 0, 0, 0, 10, OS_OPT_TIME_DLY, & err );
		if(data_line >= 240)
			data_line = 0;
	}
}

