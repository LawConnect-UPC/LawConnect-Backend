Feature: Comunicación entre Usuarios y Abogados

  Scenario: Comunicación en el Chat del Caso
    Given el usuario tiene un caso legal abierto
    When accede al chat del caso correspondiente
    Then puede enviar mensajes al abogado asignado y recibir respuestas

  Scenario: Aceptación de Condiciones y Precio
    Given el usuario ha discutido las condiciones y el precio con el abogado
    When está satisfecho con los términos propuestos
    Then puede aceptarlos y proceder con la asesoría legal
