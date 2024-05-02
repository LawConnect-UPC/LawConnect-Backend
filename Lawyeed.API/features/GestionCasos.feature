Feature: Creación y Gestión de Casos Legales

  Scenario: Creación de un Nuevo Caso
    Given el usuario necesita asesoramiento legal
    When accede a la opción de crear un nuevo caso
    Then puede ingresar los detalles del caso y crearlo

  Scenario: Gestión de Casos Existente
    Given el usuario tiene casos legales creados
    When accede a la sección de "Mis Casos"
    Then puede ver una lista de todos sus casos y gestionarlos según sea necesario
