# Checkers Game Application

A Windows Forms-based Checkers game with human vs. human and human vs. computer modes, persistent score tracking, and customizable board size and highlight colors. Designed with clean separation of concerns, testable game logic, and secure password handling.

## 🚀 Project Overview

**Language & Framework:** C# on .NET Framework, Windows Forms UI.

**Core Layers:**
- **UI Layer:** `FormGame`, `FormPlayer`, `FormLogIn`, `FormResetPassword`, etc.
- **Board Management:** `GameBoardManager` handles dynamic board rendering and resizing.
- **Game Logic:** `GameLogicManager` generates legal moves, enforces rules, and checks win conditions.
- **Flow & AI:** `GameFlowManager` orchestrates turns, move execution, win detection, and integrates a simple minimax-like AI.
- **Data Access:** `SqlGameResultRepository` persists players, scores, and game histories in SQL Server, with secure PBKDF2 password hashing in `PasswordHelper`.

## ✨ Key Features

- **Human vs. Human & Human vs. Computer:** Select game mode with a click.
- **Resizable Board:** Choose 6×6, 8×8, or 10×10 board at runtime; UI updates seamlessly.
- **Highlight Options:** Customize legal-move highlight color.
- **AI Opponent:** Adjustable search depth (default 6) for computer moves.
- **Secure Authentication:** Salted PBKDF2‐SHA256 password hashing and legacy‐to‐hash migration.
- **Persistent Scores & History:** Win/loss counts, scoring, and full game logs stored in a SQL database.
- **Statistics Dashboard:** View per‐player win/loss percentages with interactive pie charts.
- **Admin Functions:** Delete game records and refresh player statistics.

## 📦 Architecture & Design

1. **Loose Coupling & Dependency Injection**  
   - UI forms depend on interfaces/functional callbacks rather than hard‐coded singletons.  
   - Core managers receive dependencies via constructors for testability.

2. **Single Responsibility**  
   - Each class has a clear, narrow responsibility (e.g., `GameBoardManager` only manages UI board, no game rules).

3. **Secure Data Handling**  
   - `PasswordHelper` implements industry‐standard PBKDF2 with SHA-256 and iterative hashing.  
   - One‐way hashing with salt, hex‐encoded storage, legacy fallback and automatic migration.

4. **Data Layer Abstraction**  
   - `SqlGameResultRepository` encapsulates all SQL queries with helper methods (`ExecuteNonQuery`, `QuerySingle<T>`, `ExecuteSelect`).

5. **AI Integration**  
   - `GameFlowManager` calls `AiEngine` for move evaluation; decoupled so you can swap in more advanced algorithms later.

## 🛠 Setup & Running

**Requirements:**  
- .NET Framework 4.7.2 or later  
- SQL Server (LocalDB or full edition)  
- Visual Studio 2019/2022  

**Database:**  
1. Run provided DDL script (`schema.sql`) to create **Player**, **Games**, and **ErrorLog** tables.  
2. Update connection string in `App.config` to point to your SQL instance.

**Building:**  
1. Open `Checkers.sln` in Visual Studio.  
2. Restore NuGet packages (if any).  
3. Build the solution (Ctrl+Shift+B).

**Running:**  
- Press F5 or run `Checkers.exe`.  
- Login or register a new user.  
- Select game mode and enjoy!
