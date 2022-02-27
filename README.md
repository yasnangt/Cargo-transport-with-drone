# Cargo-transport-with-drone #
<p align="center" > <img src="https://media.giphy.com/media/W1fFHj6LvyTgfBNdiz/giphy.gif" width="400" height="200" > </p>
##In this project, we create a Windows Form App for a prototype of a drone delivery system.##

#Parts of Project#
The project consists of 4 parts.

In the first part, we are connecting to Arduino on a drone by Bluetooth module(HC-06). After that, we can control clamps manually. In the second part, we are connecting to the drone(DJI-TELLO). After that, we can send commands to the drone with UDP. In the third part, we are creating registration for delivery. We use SQL to create a registry on our server. In the fourth part, we are testing our delivery system. We choose one cargo package to deliver.

##Steps of Delivery##
1-The drone takes off from the cargo center

2-The drone goes to the target location from DB

3-The drone gets down for drop to package

4-The clamps open to drop the package

5-The drone takes off

6-The drone turns back to the cargo center

7-The drone lands on the cargo center




<p align="center" > <img src="https://media.giphy.com/media/Ci9giBKfL0B1u/giphy.gif" width="300" height="150" > </p>
