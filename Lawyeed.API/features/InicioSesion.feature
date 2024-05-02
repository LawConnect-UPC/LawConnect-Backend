Feature: Inicio de sesión

  Scenario: Inicio de sesión satisfactorio
    Given el usuario se encuentre en el inicio de sesión
    When ingrese sus credenciales correctos
    Then inicia sesión en su cuenta

  Scenario: Error en inicio de sesión
    Given el usuario se encuentre en el inicio de sesión
    When ingrese sus credenciales incorrectas
    Then aparece un párrafo de que la cuenta que ha ingresado es incorrecta
