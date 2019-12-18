#include "image.h"

/*初始化队列*/
void InitQueue(Queue Q)
{
	if(Q == NULL)
	{
		printf("Q is NULL\n");
		return;
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
uint8 EnQueue(Queue Q)
{
	if(IsFullQ(Q))
		return 0;
	Q->size++;
	Q->Rear = Scc(Q->Rear);
	return Q->Rear;
}
/*出队*/
uint8 DeQueue(Queue Q)
{
	if(IsEmpty(Q))
		return 0;
	Q->size--;
	Q->Fornt = Scc(Q->Fornt);
	return Q->Fornt;
}


