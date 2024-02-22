let user = null;
let connection = null;
const modal = document.getElementById("modal");
const backdrop = document.getElementById("backdrop");
const connectionStatus = document.getElementById("connection-status");
updateConnectionStatus();

function login(event) {
  event.preventDefault();
  const userName = event.target.userName.value;
  const loginItem = document.getElementById("login");
  loginItem.innerText = `Logged as: ${userName}`;
  loginItem.classList.add("info");
  closeModal();
  connect(userName);
}

function showModal() {
  modal.style.display = "block";
  backdrop.style.display = "block";
}

function closeModal() {
  modal.style.display = "none";
  backdrop.style.display = "none";
}

function connect(user) {
  connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5101/notifyhub?user_id=" + user)
    .withAutomaticReconnect()
    .build();

  updateConnectionStatus();

  connection.on("ReceiveNotification", (message) => {
    let content = document.getElementById("notification-content");
    content.innerText = message;
    notification.style.display = "flex";
    setTimeout(() => {
      notification.style.display = "none";
      content.innerText = "";
    }, 5000);
  });

  connection.start()
    .then(() => {
      updateConnectionStatus();
    })
    .catch((e) => {
      console.warn(e);
      updateConnectionStatus();
    })
    .finally(() => {
    });
}

function updateConnectionStatus() {
  let status = "no connection";
  if (connection) {
    status = connection.ut;
  }
  connectionStatus.innerText = status;
}

function notify() {
  if (!connection || connection.state !== "Connected") {
    alert("Please Log in.");
    return;
  }
  const userId = document.getElementById("userId").value;
  const message = document.getElementById("message").value;
  if (!userId || !message) {
    alert("Please insert notification data");
    return;
  }
  try {
    connection.invoke("SendNotification", userId, message);
  } catch (err) {
    console.error(err);
  }
}

const notifyButton = document.getElementById("notify");
notifyButton.addEventListener("click", notify);

const loginButton = document.querySelector(".loginButton");
loginButton.addEventListener("click", showModal);

backdrop.addEventListener("click", closeModal);

const userForm = document.querySelector(".user-form");
userForm.addEventListener("submit", login);
