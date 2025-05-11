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

| Config File        | Hidden Units | Layers | Batch Size | Learning Rate | Result |
|--------------------|--------------|--------|------------|----------------|--------|
| `config.yaml`      | 128          | 2      | 64         | 0.0003         | ✅ Stable baseline |
| `config_fast.yaml` | 128          | 2      | 128        | 0.001          | ⭐ **Best overall** |
| `config_large.yaml`| 256          | 3      | 64         | 0.0003         | ✅ Strong but slower |
| `config_small.yaml`| 64           | 1      | 64         | 0.0003         | ❌ Did not learn |

---

## 📊 TensorBoard Results

| Metric           | Best Config          | Notes |
|------------------|----------------------|-------|
| **Cumulative Reward** | `config_fast.yaml`     | Quickly reaches ~1.1 |
| **Episode Length**    | `config_fast.yaml`     | Efficient episodes (~27 steps) |
| **Policy Loss**       | `config_fast.yaml`     | Lowest and most stable |
| **Value Loss**        | `config_fast.yaml`     | Best reward prediction accuracy |

---

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

## 📸 Screenshots



## 🙌 Credits
Created by JoanWaweru

