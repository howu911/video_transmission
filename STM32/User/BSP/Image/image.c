#include "image.h"


void check_start(void)
{
	uint8_t buff[1] = {0};
	while (DEF_TRUE){
		while(getSn_SR(SOCK_UDPS) == SOCK_CLOSED)
		{
			socket(SOCK_UDPS,Sn_MR_UDP,local_port,0);
		}

		if(getSn_IR(SOCK_UDPS) & Sn_IR_RECV)
		{
			setSn_IR(SOCK_UDPS, Sn_IR_RECV);                                     /*清接收中断*/
			if((getSn_RX_RSR(SOCK_UDPS))>0)                                    /*接收到数据*/
			{
				recvfrom(SOCK_UDPS,buff, 1, remote_ip,&remote_port);               /*W5500接收计算机发送来的数据*/
				if(buff[0] == 0x01)
				{
					//sendto(SOCK_UDPS,AckData, 1, remote_ip, remote_port);                /*W5500把接收到的数据发送给Remote*/
					break;
				}						
			}
		}
	}
}

void check_stop(void)
{
	uint8_t buff[1] = {0};
	while (DEF_TRUE){
		while(getSn_SR(SOCK_UDPS) == SOCK_CLOSED)
		{
			socket(SOCK_UDPS,Sn_MR_UDP,local_port,0);
		}

		if(getSn_IR(SOCK_UDPS) & Sn_IR_RECV)
		{
			setSn_IR(SOCK_UDPS, Sn_IR_RECV);                                     /*清接收中断*/
			if((getSn_RX_RSR(SOCK_UDPS))>0)                                    /*接收到数据*/
			{
				recvfrom(SOCK_UDPS,buff, 1, remote_ip,&remote_port);               /*W5500接收计算机发送来的数据*/
				if(buff[0] == 0x08)
				{
					//sendto(SOCK_UDPS,AckData, 1, remote_ip, remote_port);                /*W5500把接收到的数据发送给Remote*/
					close(SOCK_UDPS);
					break;
				}						
			}
		}
	}
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

