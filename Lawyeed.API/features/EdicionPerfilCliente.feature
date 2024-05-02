Feature: Edición de Perfil de Usuario

  Scenario: Edición de Detalles Personales
    Given que un usuario desea actualizar su información en LawConnect
    When accede a la sección de edición de perfil
    Then puede modificar campos como nombre, apellido, dirección y número de teléfono
