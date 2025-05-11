# 🧠 BallAgent Reinforcement Learning Project (Unity ML-Agents)

This project explores reinforcement learning using Unity's ML-Agents toolkit.  
The agent is a blue ball trained to **hit a moving green cube** while avoiding **red obstacles**.

---

## 🕹️ Gameplay Summary

- **Agent**: Blue ball
- **Goal**: Reach the green target cube
- **Environment**: Spawns 1–3 red cylindrical obstacles at random
- **Mechanics**:
  - Reward for hitting the cube
  - Penalty if it falls off the platform
  - Randomized target and obstacle positions every episode

---

## 🧪 Training Setup

- **Framework**: Unity ML-Agents (PPO)
- **Observations**: Agent position, velocity, and target position
- **Actions**: Continuous control (move in X and Z directions)

---

## 🧠 Hyperparameter Experiments

We trained 4 different models with varying network sizes and training speeds.

| Config File        | Colour    | Hidden Units | Layers | Batch Size | Learning Rate | Result |
|--------------------|-----------|--------------|--------|------------|---------------|--------|
| `config.yaml`      | 🩶 Grey   | 128      | 2      | 64         | 0.0003         | ✅ Stable baseline |
| `config_fast.yaml` | 💙 Blue   |     128      | 2      | 128        | 0.001          | ⭐ **Best overall** |
| `config_large.yaml`| 🩷 Pink   |     256      | 3      | 64         | 0.0003         | ✅ Strong but slower |
| `config_small.yaml`| 💛 Yellow |      64      | 1      | 64         | 0.0003         | ❌ Did not learn |

---

## 📊 TensorBoard Results

| Metric           | Best Config          | Colour    | Notes |
|------------------|----------------------|-------|------|
| **Cumulative Reward** | `config_fast.yaml`     | 💙 Blue   |Quickly reaches ~1.1 |
| **Episode Length**    | `config_fast.yaml`     | 💙 Blue   |Efficient episodes (~27 steps) |
| **Policy Loss**       | `config_fast.yaml`     | 💙 Blue   |Lowest and most stable |
| **Value Loss**        | `config_fast.yaml`     | 💙 Blue |Best reward prediction accuracy |

---

![image](https://github.com/user-attachments/assets/a3af8ab2-60df-4451-aaea-8101df7b1bac)

![image](https://github.com/user-attachments/assets/2acad0e5-91ac-4abd-8e62-7c658ab4bb51)

![image](https://github.com/user-attachments/assets/946a774a-e628-4414-a28c-f4845ee843bb)

![image](https://github.com/user-attachments/assets/4c38152d-57e4-4cfd-8356-a4821efea10b)



## ✅ Best Model

**`config_fast.yaml`** was the best performing configuration, offering the fastest and most stable training.  
It combined a higher learning rate with larger batch and buffer sizes for efficiency without instability.

---

## 🚀 How to Train Your Own

1. Clone the project and install ML-Agents:
```bash
pip install mlagents
```

2. Train the agent:

```bash
mlagents-learn config/config_fast.yaml --run-id=fast_net --train
```

3. View training progress:
```bash
tensorboard --logdir=results
```

5. After training, assign the .nn model to the agent’s Behavior Parameters in Unity.

## 📁 Project Structure
```bash
Assets/            # Unity scenes, scripts, prefabs
config/            # ML-Agent YAML configurations
results/           # TensorBoard logs and trained models
```

## 📸 Video Recording
[![Watch the demo](https://img.youtube.com/vi/bduEMDJOCwU/0.jpg)](https://youtu.be/bduEMDJOCwU)

## 🙌 Credits
Created by JoanWaweru

