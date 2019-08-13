# WebApplication4
## Web Service orquestador 

Servicio web ESB, el cual se encarga de orquestar cada una de las solicitudes desde cada uno de los MicroServicios, analizar, comunicar y responder dichas solicitudes.


# Clases

La clase principal y la que realiza la orquestacion como tal se llamada `'ESB_autos'`. En esta podemos encontrar cada una de las referencias Web al resto de micro servicios de la siguiente manera: 

```csharp
/*
	Instancia global para el WS de pilotos
*/

WS_Piloto.WebService1SoapClient ServicoPilotos = new WS_Piloto.WebService1SoapClient();

```
Ademas de contar con los **WebMethod** necesarios para la comunicación entre cada uno de los micro servicios.  

|           |Metodo			|Parametros			| Return		|
|-----------|---------------|--------------------|--------------|
|**Pilotos**	|'NuevoPiloto'	|`String name` `String tel` `String marca` `String linea` `String placa`            | String
|**Pilotos**	|'TerminarViajePiloto'	|`int idPiloto`            | String
|**Clientes**	|'NuevoUsuario'	|`String user` `String tel` `String unique` `String clave`            | String
|**Clientes**	|'IniciarSesion'	|`String user` `String clave`            | String
|**Clientes**	|'SolicitarViaje'	|`int idUsuario` `String Origen` `String Destino`            | String
|**Clientes**	|'TerminarViajeUsuario'	|`int idUsuario`            | String
|**Rastreo**	|'ConsultarViajeUsuario'	|`int idUsuario`            | String
|**Rastreo**	|'ConsultarViajeUsuario'	|`int idPiloto`            | String


## Formato Respuesta

El formato de respuesta para cada uno de los **WebMethod** esta definido como una cadena de caracteres _string_, sin embargo el formato de esta cadena es _XML_ de la siguiente manera:

```xml
<?xml version="1.0" encoding="utf-16" standalone="yes"?>
<Rastreo_Viaje>
	<Nombre>
		<![CDATA[Joaquin]]>
	</Nombre>
	<Cliente>
		<![CDATA[Alan Bautista]]>
	</Cliente>
	<Marca_vehiculo>
		<![CDATA[Mitsubishi]]>
	</Marca_vehiculo>
	<Linea_vehiculo>
		<![CDATA[Lancer]]>
	</Linea_vehiculo>
	<Placa_vehiculo>
		<![CDATA[324YKG]]>
	</Placa_vehiculo>
	<Origen>
		<![CDATA[zona12]]>
	</Origen>
	<Destino>
		<![CDATA[casa]]>
	</Destino>
	<Fecha>
		<![CDATA[13/08/2019 00:00:00]]>
	</Fecha>
	<Codigo>
		<![CDATA[0]]>
	</Codigo>
</Rastreo_Viaje>
```

Dicho XML contiene información relevante para cada una de las solicitudes, sin embargo todas las solicitudes manejan el nodo ```<Codigo>``` el cual contiene el resultado de la solicitud siendo:

|Codigo |Estado|
|-------|------|
|0		| Éxito|
|1		| Error|

