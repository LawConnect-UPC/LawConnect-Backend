Feature: Gestión de cuentas de usuario

  Scenario: Registro de cuentas con correo
    Given que el usuario ingrese a la aplicación y esté en la sección de registro de cuenta
    When escoja el tipo de usuario entre postulante o reclutador, ingrese datos de registro de cuenta y sean correctos según validaciones
    Then la cuenta se creará y se envía un código de activación de cuenta a su correo

  Scenario: Registro incorrecto
    Given que el usuario ingrese a la aplicación y esté en la sección de registro de cuenta
    When ingrese sus credenciales de manera incorrecta
    Then la cuenta no se creará y aparece un mensaje que las credenciales están incorrectas

  Scenario: Inicio de sesión satisfactorio
    Given el usuario se encuentre en el inicio de sesión
    When ingrese sus credenciales correctos
    Then inicia sesión en su cuenta

  Scenario: Error en inicio de sesión
    Given el usuario se encuentre en el inicio de sesión
    When ingrese sus credenciales incorrectas
    Then aparece un párrafo de que la cuenta que ha ingresado es incorrecta
