Feature: Edición de Perfil de Abogado

  Scenario: Edición de Detalles Personales
    Given que un abogado desea actualizar su información en LawConnect
    When accede a la sección de edición de perfil
    Then puede modificar campos como nombre, apellido, dirección y número de teléfono

  Scenario: Actualización de Detalles Profesionales
    Given que un abogado desea actualizar su información profesional en LawConnect
    When accede a la sección de edición de perfil
    Then puede editar campos como área de especialización, experiencia laboral y educación

