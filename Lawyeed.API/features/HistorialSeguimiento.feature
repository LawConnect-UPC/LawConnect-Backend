Feature: Historial y Seguimiento de Casos

  Scenario: Visualización de Historial de Casos
    Given el usuario quiere revisar su historial de casos
    When accede a la sección de historial de casos
    Then puede ver una lista de todos los casos anteriores con detalles de su estado actual

  Scenario: Actualización de Estado del Caso
    Given el estado de un caso ha cambiado
    When el abogado actualiza el estado del caso
    Then el usuario recibe una notificación y puede ver el cambio en el historial de casos
