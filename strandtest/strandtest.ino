#include <Adafruit_NeoPixel.h>
#ifdef __AVR__
  #include <avr/power.h>
#endif

#define PIN 2
#define SUBPIN 8
#define LED_SIZE 30
#define LED_OFF strip.Color(0,0,0)

char read_direction[2];
char read_red[4];
char read_green[4];
char read_blue[4];

String InputString = "";
boolean StringComplete = 0;
boolean SetValidData = 0;

// Parameter 1 = number of pixels in strip
// Parameter 2 = Arduino pin number (most are valid)
// Parameter 3 = pixel type flags, add together as needed:
//   NEO_KHZ800  800 KHz bitstream (most NeoPixel products w/WS2812 LEDs)
//   NEO_KHZ400  400 KHz (classic 'v1' (not v2) FLORA pixels, WS2811 drivers)
//   NEO_GRB     Pixels are wired for GRB bitstream (most NeoPixel products)
//   NEO_RGB     Pixels are wired for RGB bitstream (v1 FLORA pixels, not v2)
//   NEO_RGBW    Pixels are wired for RGBW bitstream (NeoPixel RGBW products)
Adafruit_NeoPixel strip = Adafruit_NeoPixel(LED_SIZE, PIN, NEO_GRB + NEO_KHZ800);
Adafruit_NeoPixel sub_strip = Adafruit_NeoPixel(LED_SIZE, SUBPIN, NEO_GRB + NEO_KHZ800);
// IMPORTANT: To reduce NeoPixel burnout risk, add 1000 uF capacitor across
// pixel power leads, add 300 - 500 Ohm resistor on first pixel's data input
// and minimize distance between Arduino and first pixel.  Avoid connecting
// on a live circuit...if you must, connect GND first.

void setup() {
  // This is for Trinket 5V 16MHz, you can remove these three lines if you are not using a Trinket
  #if defined (__AVR_ATtiny85__)
    if (F_CPU == 16000000) clock_prescale_set(clock_div_1);
  #endif
  // End of trinket special code
  Serial.begin(9600);

  strip.begin();
  strip.show(); // Initialize all pixels to 'off'
  sub_strip.begin();
  sub_strip.show();
}

void loop() {
  //color_arr(strip.Color(255, 0, 0), 0, LED_SIZE/3);
  //color_arr(strip.Color(0, 255, 0), LED_SIZE/3, LED_SIZE*2/3);
  //color_arr(strip.Color(0, 0, 255), LED_SIZE*2/3, LED_SIZE);
  // Some example procedures showing how to display to the pixels:
  //colorWipe(strip.Color(255, 0, 0), LED_SIZE/3); // Red
  //colorWipe(strip.Color(0, 255, 0), LED_SIZE*2/3); // Green
  //colorWipe(strip.Color(0, 0, 255), LED_SIZE); // Blue
//colorWipe(strip.Color(0, 0, 0, 255), 50); // White RGBW
  // Send a theater pixel chase in...
  //theaterChase(strip.Color(127, 127, 127), LED_SIZE/3); // White
  //theaterChase(strip.Color(127, 0, 0), LED_SIZE*2/3); // Red
  //theaterChase(strip.Color(0, 0, 127), LED_SIZE); // Blue
  half();
  //sub_color_arr(strip.Color(255, 0, 156), 0, LED_SIZE/2);
  //flash2();

  
  //rainbow(20);
  //rainbowCycle(20);
  //theaterChaseRainbow(LED_SIZE);
}

void half(){
  flash();
}

void flash2(){
  if (Serial.available() > 0) {
    // read the oldest byte in the serial buffer:
    //incomingByte = Serial.read();
    char wait = Serial.read();
    int incomingByte;
    String sig;
    sig.concat(wait);
    sig.substring(0,1).toCharArray(read_direction,2);
    sig.substring(1,4).toCharArray(read_red,4);
    sig.substring(4,7).toCharArray(read_green,4);
    sig.substring(7,9).toCharArray(read_blue,4);
    int real_red = atoi(read_red);
    int real_green = atoi(read_green);
    int real_blue = atoi(read_blue);
    //Serial.print(read_direction);
    //Serial.println(read_red);
    //Serial.println(real_red);
    if(read_direction[0] == 'L'){
      color_arr(strip.Color(255, 0, 156), 0, LED_SIZE/2);
      //color_arr(strip.Color(real_red, real_green, real_blue), 0, LED_SIZE/2);
    }
    else if(read_direction[0] == 'R'){
      color_arr(strip.Color(0, 0, 255), LED_SIZE/2, LED_SIZE);
      //color_arr(strip.Color(real_red, real_green, real_blue), LED_SIZE/2, LED_SIZE);
    }
    else if(read_direction[0] == 'E'){
      off_arr(0, LED_SIZE);
    }
    sig = "";
  }
}

void flash(){
   if (StringComplete && InputString.length()) {
    Serial.println(InputString);
    //color_arr(strip.Color(0, 0, 255), LED_SIZE/2, LED_SIZE);
    InputString.substring(0,1).toCharArray(read_direction,2);
    InputString.substring(1,4).toCharArray(read_red,4);
    InputString.substring(4,7).toCharArray(read_green,4);
    InputString.substring(7,10).toCharArray(read_blue,4);
    int real_red = atoi(read_red);
    int real_green = atoi(read_green);
    int real_blue = atoi(read_blue);
    if(read_direction[0] == 'L'){
      //color_arr(strip.Color(255, 0, 156), 0, LED_SIZE/2);
      color_arr(strip.Color(real_red, real_green, real_blue), 0, LED_SIZE/2);
    }
    else if(read_direction[0] == 'R'){
      //color_arr(strip.Color(0, 0, 255), LED_SIZE/2, LED_SIZE);
      color_arr(strip.Color(real_red, real_green, real_blue), LED_SIZE/2, LED_SIZE);
    }
    else if(read_direction[0] == 'E'){
      off_arr(0, LED_SIZE);
      sub_off_arr(0, LED_SIZE);
    }
    else if(read_direction[0] == 'Q'){
      sub_color_arr(strip.Color(real_red, real_green, real_blue), 0, LED_SIZE/2);
    }
    else if(read_direction[0] == 'W'){
      sub_color_arr(strip.Color(real_red, real_green, real_blue), LED_SIZE/2, LED_SIZE);
    }
    InputString = "";
    StringComplete = 0;
  }
}

void serialEvent() {
  char InChar;
  SetValidData = 1;
  
  while (Serial.available()&& SetValidData) {
    InChar = Serial.read();
    //rainbow(20);
    if (SetValidData) {
      if (InChar == '\n') {
        //color_arr(strip.Color(255, 0, 156), 0, LED_SIZE/2);
        StringComplete = 1;
        SetValidData = 0;
        return;
      }
      if (InputString.length() < 11){
        //color_arr(strip.Color(0, 0, 156), 0, LED_SIZE/2);
        InputString += InChar;
        return;
      }
      else {
        //color_arr(strip.Color(0, 255, 0), 0, LED_SIZE/2);
        InputString = "";
        SetValidData = 0;
      }
    }
  }
}

void sub_off_arr(uint8_t kara, uint8_t made){
  for(uint16_t i=kara;i<made;i++){
    sub_strip.setPixelColor(i, LED_OFF);
    sub_strip.show();
  }
}

void off_arr(uint8_t kara, uint8_t made){
  for(uint16_t i=kara;i<made;i++){
    strip.setPixelColor(i, LED_OFF);
    strip.show();
  }
}

void sub_color_arr(uint32_t c, uint8_t kara, uint8_t made){
  for(uint16_t i=kara; i<made;i++){
    sub_strip.setPixelColor(i, c);
    sub_strip.show();
    //delay(20);
  }
}

void color_arr(uint32_t c, uint8_t kara, uint8_t made){
  for(uint16_t i=kara; i<made;i++){
    strip.setPixelColor(i, c);
    strip.show();
    //delay(20);
  }
}

// Fill the dots one after the other with a color
void colorWipe(uint32_t c, uint8_t wait) {
  for(uint16_t i=0; i<strip.numPixels(); i++) {
    strip.setPixelColor(i, c);
    strip.show();
    delay(wait);
  }
}

void rainbow(uint8_t wait) {
  uint16_t i, j;

  for(j=0; j<256; j++) {
    for(i=0; i<strip.numPixels(); i++) {
      strip.setPixelColor(i, Wheel((i+j) & 255));
    }
    strip.show();
    delay(wait);
  }
}

// Slightly different, this makes the rainbow equally distributed throughout
void rainbowCycle(uint8_t wait) {
  uint16_t i, j;

  for(j=0; j<256*5; j++) { // 5 cycles of all colors on wheel
    for(i=0; i< strip.numPixels(); i++) {
      strip.setPixelColor(i, Wheel(((i * 256 / strip.numPixels()) + j) & 255));
    }
    strip.show();
    delay(wait);
  }
}

//Theatre-style crawling lights.
void theaterChase(uint32_t c, uint8_t wait) {
  for (int j=0; j<10; j++) {  //do 10 cycles of chasing
    for (int q=0; q < 3; q++) {
      for (uint16_t i=0; i < strip.numPixels(); i=i+3) {
        strip.setPixelColor(i+q, c);    //turn every third pixel on
      }
      strip.show();

      delay(wait);

      for (uint16_t i=0; i < strip.numPixels(); i=i+3) {
        strip.setPixelColor(i+q, 0);        //turn every third pixel off
      }
    }
  }
}

//Theatre-style crawling lights with rainbow effect
void theaterChaseRainbow(uint8_t wait) {
  for (int j=0; j < 256; j++) {     // cycle all 256 colors in the wheel
    for (int q=0; q < 3; q++) {
      for (uint16_t i=0; i < strip.numPixels(); i=i+3) {
        strip.setPixelColor(i+q, Wheel( (i+j) % 255));    //turn every third pixel on
      }
      strip.show();

      delay(wait);

      for (uint16_t i=0; i < strip.numPixels(); i=i+3) {
        strip.setPixelColor(i+q, 0);        //turn every third pixel off
      }
    }
  }
}

// Input a value 0 to 255 to get a color value.
// The colours are a transition r - g - b - back to r.
uint32_t Wheel(byte WheelPos) {
  WheelPos = 255 - WheelPos;
  if(WheelPos < 85) {
    return strip.Color(255 - WheelPos * 3, 0, WheelPos * 3);
  }
  if(WheelPos < 170) {
    WheelPos -= 85;
    return strip.Color(0, WheelPos * 3, 255 - WheelPos * 3);
  }
  WheelPos -= 170;
  return strip.Color(WheelPos * 3, 255 - WheelPos * 3, 0);
}
