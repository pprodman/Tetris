# Proyecto Tetris en Unity 🕹️

## 📌 Releases

### 1. [Actividad 2A](https://github.com/pprodman/Tetris/releases/tag/v.1.0): Introducción
- Creación de proyecto
- Configuración de escena:
  - Camara principal.
  - Sprites (block y  border).
- Piezas:
  -   Prefabs de bloques para las piezas.
  - `Spawner`: objeto encargado de crear piezas nuevas.
- Scripts:
  - `Board`: Este Script contendrá una clase estática que almacenará el estado de la partida y se encargará de asegurar la consistencia de la misma.
  - `Piece`: Este script irá asociado a los Prefabs de las piezas (a todos ellos) y contendrá los movimientos de las piezas.
 
### 2. [Actividad 2B](https://github.com/pprodman/Tetris/releases/tag/v1.1): Optimización
- Optimización de nuestro juego de Tetris con object pooling.
  - El Spawner creará una pieza de cada tipo nada más comenzar y sólo dejará habilitada y visible una pieza (la primera que caerá).
  - El tablero estará lleno de Blocks ocultos / No visibles.
  - Cuando la pieza se detiene queda por encima de ciertos Blocks del tablero. Estos Blocks en este momento se harán visibles y la pieza se moverá a una ubicación no visible y se deshabilitará su Script para poder aprovecharla de nuevo. De modo que parecerá que la pieza se ha quedado fija, cuando realmente la habremos movido para reutilizarla.
  - A continuación se habilitará una nueva pieza para que empiece a bajar.
- El resto del código deberá adaptarse en consecuencia a estos nuevos cambios.

### 3. Actividad 2C
