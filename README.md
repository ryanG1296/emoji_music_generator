# emoji-music-generator
# Emoji-Based Generative Music System

This repository contains the source code and documentation for a Unity-based generative music system that uses emoji inputs to create custom music. The project combines Unity 2D for interactive design and Python for automated interaction with the Sonauto.ai platform.

---

## Features

### Unity Scripts
1. **`cameramove.cs`**
   - **Target:** Main Camera
   - **Function:** Enables swipe gestures to simulate mobile device interaction, allowing users to navigate between pages.

2. **`gravity.cs`**
   - **Target:** All selectable emojis
   - **Function:** Adds gravity effects for stackable emoji selection, generates a small icon of the selected emoji at the top of the screen, tracks user selections, and updates the choices to a backend text file.

3. **`eventforevent.cs`**
   - **Target:** Event system
   - **Function:** Monitors updates from `gravity.cs`, processes the selected emojis based on predefined mappings, generates a new prompt file, and triggers the `demo3.exe` program to interact with the Sonauto.ai website.

4. **`loadingbar.cs`**
   - **Target:** UI Loading Bar
   - **Function:** Controls the animated loading bar that reflects progress during music generation.

5. **`lyrics.cs`**
   - **Target:** TextMeshPro Component
   - **Function:** Displays scrolling lyrics retrieved from the Sonauto.ai-generated results on the screen.

### Python Script
- **`webpagemng.py`**
  - **Function:** Automates user interactions with the Sonauto.ai platform using Chrome WebDriver. This script logs into a temporary personal account (specific account name and password removed for anonymity; please open the code and insert your own account to run), submits the prompt file, retrieves the generated lyrics, and plays the music on the webpage.
- **Note:** This Python script needs to be transmitted into an exe file and renamed as "demo3.exe" to run in the Unity program. 


---

## System Requirements

To run the project on another computer, ensure the following tools and libraries are installed:

### Unity
- **UnityEngine**
- **UnityEngine.Video**
- **TextMeshPro** (Installable via Unity's Package Manager)

### Python
- **selenium** (for automated browser interactions)

### Additional Tools
- **Google Chrome** (compatible with the version of ChromeDriver used)
- **.NET Framework** (required for Unity's C# scripts)

---

## How to Use

1. Clone the repository to your local machine.
2. Open the Unity project in a compatible Unity Editor version.
3. Install all required Unity packages (e.g., TextMeshPro).
4. Ensure Python and Selenium are installed for running the `webpagemng.py` script:
   ```bash
   pip install selenium
5. Replace the ChromeDriver path in webpagemng.py with the path to your local ChromeDriver installation.
6. Run the Unity project and select emojis to create music.
7. The generated music will be played automatically, and the lyrics will scroll on the screen.
