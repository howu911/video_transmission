#ifndef __IMAGE_H
#define __IMAGE_H

#include "stm32f10x.h"
#include "./ov7725/bsp_ov7725.h"
#include "W5500_conf.h"
#include "socket.h"
#include <stdio.h>
#include "w5500.h"
#include  <os.h>

extern uint8  remote_ip[4];											/*‘∂∂ÀIPµÿ÷∑*/
extern uint16 remote_port;


void SendImageToComputer(uint16_t width, uint16_t height);
#endif


