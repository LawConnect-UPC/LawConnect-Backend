Feature: Búsqueda de Perfil de Abogado por Nombre

  Scenario: Búsqueda por Nombre de Abogado
    Given que un usuario desea encontrar a un abogado específico en LawConnect
    When utiliza la función de búsqueda e ingresa el nombre del abogado
    Then se muestran los perfiles de los abogados que coinciden con el nombre proporcionado
