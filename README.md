# 🛍️ E-commerce API Project

### **Introduction**

Welcome to the documentation for the **E-commerce API**. This API serves as the robust backend for a modern e-commerce platform, offering features like **product management, secure authentication (RBAC), real-time updates, payment processing (Stripe), advanced caching, and a user following system**. It's built with **ASP.NET Core** and designed for high performance and scalability.

---

### **✨ Key Features**

* **Authentication & Authorization:** Secure user authentication using **JWTs** with **Role-Based Access Control (RBAC)** to manage different permission levels (e.g., `Admin`, `User`).
* **Product & Catalog Management:** Comprehensive CRUD operations for products, categories, and inventory.
* **Order Processing:** Lifecycle management for orders, from cart creation to fulfillment.
* **Payment Integration:** Seamless payment processing using the **Stripe API**.
* **Real-time Updates:** Utilizes **WebSockets** (SignalR) for real-time order status updates, inventory changes, and notifications.
* **Performance & Caching:** Implements **Redis** for efficient data caching to improve response times and reduce database load on high-traffic endpoints.
* **Following System:** A social commerce feature allowing users/customers to **follow** their favorite sellers or brands.
* **Search & Filtering:** Advanced search capabilities.

---

### **🚀 Getting Started**

#### **Prerequisites**
* **Docker**
* **Docker Compose**

#### **Installation**

1.  **Clone the repository:**
    ```bash
    git clone [your-repo-link]
    cd [your-project-directory]
    ```

2.  **Set up Environment Variables:**
    Create a file named `.env` in the root directory and populate it with your configuration.

    ```bash
    # --- Database Configuration ---
    DB_HOST=db_service_name # Use the Docker service name if connecting from the API container
    DB_PORT=5432
    DB_USER=ecommerce_user
    DB_PASS=your_db_password
    DB_NAME=ecommerce_api

    # --- Authentication & Security ---
    JWT_SECRET=a_very_secret_key_for_jwts
    JWT_EXPIRY=1d
    ADMIN_ROLE_ID=1
    CUSTOMER_ROLE_ID=2

    # --- Payment (Stripe) ---
    STRIPE_SECRET_KEY=sk_test_xxxxxxxxxxxxxxxxxxxx
    STRIPE_WEBHOOK_SECRET=whsec_xxxxxxxxxxxxxxx

    # --- Caching (Redis) ---
    REDIS_URL=redis://redis_service_name:6379 # Use the Redis Docker service name
    
    # --- Real-time (WebSockets) ---
    WS_PORT=4000

    # --- Docker Port Mapping (Example: Expose API on host port 8080) ---
    APP_PORT=8080 
    ```

#### **🐳 Running with Docker Compose**


1.  **Build and Run the Services:**
    ```bash
    docker-compose up --build -d
    ```
    *(The `-d` flag runs containers in detached mode)*

2.  **Run Migrations/Schema Setup (Inside the container):**
    You may need to execute your migration command within the running API container.
    ```bash
    docker exec [container_id_or_name] [Your command to run migrations]
    # Example: docker exec ecommerce-api npm run migrate
    ```

3.  **Start the API Server:**
    ```bash
    # Example for Node.js
    npm run dev
    # or
    npm start
    ```
    The API should now be running at `http://localhost:[Your_API_Port]`.

---




