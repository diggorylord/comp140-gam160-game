
//Defines the pins to use within this code.
#define trigPin 13
#define echoPin 12
#define led 11

//this setup is for putting the pins into their modes so they can take a reading.
void setup() {
  Serial.begin (9600); // sets the baud value to be used in the arduino.
  pinMode(trigPin, OUTPUT); // sets the trigpin value to be an output to get data.
  pinMode(echoPin, INPUT); // sets the echopin to be an input so that it can read what comes back from the trigpin.
  pinMode(led, OUTPUT); // sets the led to be an output so that it lights up under a certain circumstance.
}
// this loop is for the sensor to send out a sound wave that, if blocked, will send another wave back to give the sensor a reading for the sound wave as to how long it took to be blockee, then to calculate the distance from that value.
void loop() {
  long duration, distance; // sets the variables for duration and distance.
  digitalWrite(trigPin, LOW); //sets the trigpin to low so it doesnt read. 
  delayMicroseconds(2); //sets up a small delay between putting the value form low to high.
  digitalWrite(trigPin, HIGH); // sets it to high so it can read a value.

  delayMicroseconds(10); //delay before it sets the trigpin to low again.
  digitalWrite(trigPin, LOW);
  duration = pulseIn(echoPin, HIGH); //puts the echopin to high  so that it can recieve the input from the sound wave.
  distance = (duration/2) / 29.1; // does the calculation to calculate the distance.
  
  if (distance <= 10) // if the distance is less than 10 the LED light will come on. if not then it wont.
  { 
    digitalWrite(led,HIGH); 
  }
  else
  {
    digitalWrite(led,LOW); 
  }
  
  if (distance >= 200 || distance <= 0)// if the distance is higher than 200 or less than or equal to 0 it wont give a reading.
  {
    //Serial.println("Out of range");
  }
  else 
  {
    Serial.println(distance); // this prints the distance to the serial monitor for debugging.
    //Serial.println(" cm");
  }
  delay(30); // this adds a delay to what is sent so that it works smoother with unity.
}
