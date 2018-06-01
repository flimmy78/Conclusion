#ifndef __SYSCONFIG_H
#define __SYSCONFIG_H

#include "delay.h"
#include "AD.h"
#include "Dio.h"
#include "W25Q16.h"
#include "DAC8554.h"
#include "Globle.h"
#include "usart.h"
#include "timer.h"
#include "touchscreen.h"

//ADC转换变量
uint16_t ADC_ConvertedValue[10][6];


//小板通信定义相关量
char TSReceivedBuffer[24];
char TSReadPortsBuffer[18];
char TSSendBuffer[22]={0};
char TSwriteRegister[12]={0};
char TSSentCheckCode=0;         //待发送数据的效验码

volatile TSTASKSEL TSTaskSel = TSNON;

u8 tx1[24]={0x55,0x66,0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x66,0x55};
u8 tx2[24]={0x55,0x66,0x02,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x00,0x00,0x10,0x66,0x55};
u8 tx3[24]={0x55,0x66,0x03,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x00,0x00,0x10,0x66,0x55};
u8 tx4[24]={0x55,0x66,0x04,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x00,0x00,0x10,0x66,0x55};

u16 CO2=0;  
u16 CO2Sv=0;
u16 RoomTemp=0;
u16	RoomTempSv=245;
u16 RoomHum=0;
u16 RoomHumSv=600;

//串口2相关参数
unsigned int  ReceivedNum = 0;
char  SentBuffer[200];

//定义modbus通信参数数据
u16 AI1=0, AI2=0, AI3=0, AI4=0, AI5=0, AI6=0;
u16 DI1=0, DI2=0, DI3=0, DI4=0;
u16 AO1=0,AO2=0,AO3=0,AO4=0;
u16 DO1=0,DO2=0,DO3=0,DO4=0;
u16 PWM1_cyc=0, PWM1_duty=0, PWM2_cyc=0, PWM2_duty=0;
u16 StartCmd=0, StopCmd=0,SetValue=0;	
	
	
//定义函数	
void RCC_Configuration(void);
void IWDG_Configuration(void);
void NVIC_Configuration(void);
void LedInit(void);
void RegMap(void);
#endif
