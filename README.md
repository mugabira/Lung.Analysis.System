# Chest X-ray Analysis API with ML.NET

![.NET Version](https://img.shields.io/badge/.NET-7.0-blue)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

A RESTful API for detecting Tuberculosis and other lung diseases from chest X-ray images using C# and ML.NET.

## Table of Contents
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
  - [API Endpoints](#api-endpoints)
  - [Example Requests](#example-requests)
- [Model Training](#model-training)
- [Deployment](#deployment)
- [Contributing](#contributing)
- [License](#license)

## Features

- 🏥 **Medical Image Analysis**: Processes chest X-rays in JPG/JPEG/PNG formats
- 🔍 **Multi-Disease Detection**: Identifies TB, Pneumonia, COVID-19, or Normal cases
- 📊 **Confidence Metrics**: Returns probability scores for each diagnosis
- ⚡ **High Performance**: Optimized for concurrent prediction requests
- 🐳 **Docker Ready**: Containerized deployment support

## Getting Started

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- Trained ML model (`.zip` file)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/tb-detection-api.git
   cd tb-detection-api