Feature: Visualización de Perfiles de Abogados

  Scenario: Visualización de Detalles del Perfil de Abogado
    Given que un usuario está buscando un abogado en LawConnect
    When accede a la sección de perfiles de abogados
    Then puede ver información detallada sobre cada abogado, incluyendo su área de especialización, experiencia laboral, casos ganados y cualquier otra información relevante
