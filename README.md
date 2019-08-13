# ESB_autos
## aplicación SOA para simular los servicios de carros tipo Uber


###### Alan Bautista
###### 201212487

Desarrollo de una aplicacion que simula los servicios de Uber, dicha aplicacion esta conformada por 3 MicroServicios los cuales tienen tareas especificas, cada uno de estos MicroServicios tiene funciones especificas segun lo requiere la logica denegocio.

Los MicroServicios son los siguientes:

* Usuarios
* RastreoCarro
* Pilotos

Cada uno de estos MicroServicios estan orquestados por un ESB, el cual se encarga de distribuir cada una de las llamadas y comunicaciones entre servicios:

* WebApplication4
  * Usuarios
  * RastreoCarro
  * Pilotos
