Feature: Cerrar sesión

  Scenario: Cerrar sesión con el perfil
    Given el usuario visualiza la barra de navegación
    When seleccione el perfil
    And seleccione cerrar sesión
    Then la sesión se cierra
    And es redirigido al formulario de inicio de sesión.

  Scenario: Cerrar sesión con el menú de ajustes
    Given el usuario visualiza la barra de navegación
    When seleccione ajustes
    And seleccione cerrar sesión
    Then la sesión se cierra
    And es redirigido al formulario de inicio de sesión.
