#ifndef _BSP_I2C_GPIO_H
#define _BSP_I2C_GPIO_H

#include <inttypes.h>

/* 定义I2C总线连接的GPIO端口, 用户只需要修改下面4行代码即可任意改变SCL和SDA的引脚 */
#define GPIO_PORT_I2C	    GPIOB			               /* GPIO端口 */
#define RCC_I2C_PORT 	    RCC_APB2Periph_GPIOB		 /* GPIO端口时钟 */
#define I2C_SCL_PIN		    GPIO_Pin_6			         /* 连接到SCL时钟线的GPIO */
#define I2C_SDA_PIN		    GPIO_Pin_7			         /* 连接到SDA数据线的GPIO */


/* 定义读写SCL和SDA的宏，已增加代码的可移植性和可阅读性 */
#if 0	/* 条件编译： 1 选择GPIO的库函数实现IO读写 */
	#define I2C_SCL_1()     GPIO_SetBits(GPIO_PORT_I2C, I2C_SCL_PIN)		      /* SCL = 1 */
	#define I2C_SCL_0()     GPIO_ResetBits(GPIO_PORT_I2C, I2C_SCL_PIN)		    /* SCL = 0 */
	
	#define I2C_SDA_1()     GPIO_SetBits(GPIO_PORT_I2C, I2C_SDA_PIN)		      /* SDA = 1 */
	#define I2C_SDA_0()     GPIO_ResetBits(GPIO_PORT_I2C, I2C_SDA_PIN)		    /* SDA = 0 */
	
	#define I2C_SDA_READ()  GPIO_ReadInputDataBit(GPIO_PORT_I2C, I2C_SDA_PIN)	/* 读SDA口线状态 */
  
#else	/* 这个分支选择直接寄存器操作实现IO读写 */

    /*　注意：如下写法，在IAR最高级别优化时，会被编译器错误优化 */
	#define I2C_SCL_1()     GPIO_PORT_I2C->BSRR = I2C_SCL_PIN				          /* SCL = 1 */
	#define I2C_SCL_0()     GPIO_PORT_I2C->BRR = I2C_SCL_PIN				          /* SCL = 0 */
	
	#define I2C_SDA_1()     GPIO_PORT_I2C->BSRR = I2C_SDA_PIN				          /* SDA = 1 */
	#define I2C_SDA_0()     GPIO_PORT_I2C->BRR = I2C_SDA_PIN				          /* SDA = 0 */
	
	#define I2C_SDA_READ()  ((GPIO_PORT_I2C->IDR & I2C_SDA_PIN) != 0)	        /* 读SDA口线状态 */
  
#endif

#define I2C_WR	0		/* 写控制bit */
#define I2C_RD	1		/* 读控制bit */

void i2c_Start(void);
void i2c_Stop(void);
void i2c_SendByte(uint8_t _ucByte);
uint8_t i2c_ReadByte(void);
uint8_t i2c_WaitAck(void);
void i2c_Ack(void);
void i2c_NAck(void);
uint8_t i2c_CheckDevice(uint8_t _Address);
void i2c_CfgGpio(void);
#endif
