# 🧩 Learning Microservice Architecture with .NET

This repository offers a fundamental and important learning experience on **microservice architecture** with **.NET** technologies. Each topic is structured as a separate **solution** and the technologies used are:

- **ASP.NET Core**
- **MongoDB**
- **MSSQL**
- **RabbitMQ**
- **Event Store DB**
- **NGINX**
- **Ocelot**
- **YARP**
- **NLog**

## 🚀 Content

### 🔹 CAP Theorem
- Detailed examination of the **Consistency**, **Availability**, **Partition Tolerance** trilemma.

### 🔄 Eventual Consistency & SAGA Pattern
1. Service coordination with the **Choreography** method.
2. Centralized process control with the **Orchestration** method.

### 🔒 Strong Consistency
- **Two Phase Commit (2PC) Protocol**: Ensuring strong data consistency in distributed systems.

### 📦 Outbox & Inbox Pattern
- Message reliability with the **Outbox Pattern**.
- Guaranteed message reprocessing with the **Inbox Pattern**.

### 🌀 Idempotent Problem
- Solutions on how services handle repeatable operations.

### 📜 Event Sourcing & Event Store DB
- Event-based data storage and management.
- Practical applications with **Event Store DB**.

### 🖥️ CQRS Pattern
- Separation of **Command** and **Query** operations.

### 🔍 Service Health Monitoring
- Mechanisms for monitoring service status with **Health Check** integration.

### 🧵 Traceability in Distributed Systems
- Usage of **Traceability**, **Correlation ID**, and **NLog**.

### 🚪 API Gateway
- **API Gateway** solutions for directing all API traffic.

### ⚖️ Load Balancing
- Load balancing and scalability with **Ocelot**, **YARP**, and **NGINX**.

### 🛡️ Authentication and Authorization
- Implementations of **Authentication** and **Authorization**.

## 🛠️ Technologies Used

| Technology       | Description                          |
|------------------|---------------------------------------|
| **ASP.NET Core** | The main framework for microservices. |
| **MongoDB**      | NoSQL database solution.              |
| **MSSQL**        | Relational database solution.         |
| **RabbitMQ**     | Messaging and process management.     |
| **Event Store DB**| Event-based data storage solution.   |
| **NGINX**        | Load balancing and API Gateway.       |
| **Ocelot**       | API Gateway and load balancing.       |
| **YARP**         | High-performance API routing.         |
| **NLog**         | Logging and traceability tool.        |

## 📈 Project Structure

Each topic in the project is structured as a separate **solution**. This structure facilitates independent learning of topics and gaining hands-on experience.

### 📊 Visualization of Architectural Patterns

#### SAGA Pattern (Choreography)
```plaintext
[Service A] --> [Event Bus] --> [Service B]
```

#### SAGA Pattern (Orchestration)
```plaintext
[Orchestrator] --> [Service A]
               --> [Service B]
               --> [Service C]
```

#### CQRS Pattern
```plaintext
[Command] --> [Command Handler] --> [Domain Model]
[Query]   --> [Query Handler]   --> [Read Model]
```

#### Event Sourcing
```plaintext
[Command] --> [Aggregate] --> [Event Store]
[Query]   --> [Read Model] --> [Event Store]
```
