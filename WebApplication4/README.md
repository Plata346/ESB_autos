# WebApplication4
## Web Service orquestador 


###### Alan Bautista
###### 201212487

Este Web Service posee una Clase unica llamada [ESB_autos]




Los MicroServicios son los siguientes:

* Usuarios
* RastreoCarro
* Pilotos

Cada uno de estos MicroServicios estan orquestados por un ESB, el cual se encarga de distribuir cada una de las llamadas y comunicaciones entre servicios:

* WebApplication4
  * Usuarios
  * RastreoCarro
  * Pilotos
