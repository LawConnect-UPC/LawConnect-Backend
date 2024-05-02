Feature: Notificaciones de Mensajes en Casos

  Scenario: Recepción de Notificaciones
    Given el usuario está involucrado en un caso legal
    When el abogado o el cliente envían un mensaje dentro del chat del caso
    Then el usuario recibe una notificación informándole del nuevo mensaje
