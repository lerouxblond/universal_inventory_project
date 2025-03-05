# 🎮 Universal Game System – Modular & Scalable Inventory, Health, and Item System

## 📌 Project Overview
This project provides a **modular and reusable game system** for Unity, including **inventory, player movement, health, and item mechanics**. Designed with **ScriptableObjects** and **MVC architecture**, it ensures clean separation of concerns and scalability for various game types (**RPG, survival, adventure, etc.**).  

## ✨ Features
✅ **Universal Inventory System** – Works for **players, chests, merchants, and more**.  
✅ **Smooth Player Movement & Animation** – Basic movement logic with animation support.  
✅ **Universal Health System** – Reusable for **players, enemies, NPCs** with event-driven updates.  
✅ **Item System** – **ScriptableObjects** define weapons, consumables, and key items.  
✅ **Item Actions** – Custom behaviors like **healing, equipping, and using abilities**.  
✅ **Drag & Swap Mechanics** – Intuitive UI interactions for managing items.  

## 🏗️ Architecture & Design
This system follows the **MVC (Model-View-Controller) pattern** for maintainability and modularity:  

### 🔹 **ScriptableObjects for Data Management**
- **Inventory Data** – Stores item lists, stack limits, and availability.  
- **Health Data** – Tracks max health, current health, and health events.  
- **Item Data** – Defines item properties (type, effects, actions).  

### 🔹 **Model-View-Controller (MVC) Structure**
📂 **Model** – Defines **ScriptableObjects** for inventory, health, and items.  
📂 **View** – Handles **UI components** (health bar, inventory UI).  
📂 **Controller** – Connects **game logic, input handling, and UI updates**.  

## 🔧 Installation & Setup
1. **Clone the Repository**
   ```sh
   git clone <repository-url>
   cd <project-folder>
