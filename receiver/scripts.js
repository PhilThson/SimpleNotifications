const connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5101/notifyhub")
  .withAutomaticReconnect()
  .build();

connection.on("ReceiveNotification", (message) => {
  let content = document.getElementById("notification-content");
  content.innerText = message;
  notification.style.display = "block";
  setTimeout(() => {
    notification.style.display = "none";
    content.innerText = "";
  }, 5000);
});

connection.start();

function notify() {
  const connectionId = document.getElementById("connectionId").value;
  const message = document.getElementById("message").value;
  try {
    connection.invoke("SendNotification", connectionId, message);
  } catch (err) {
    console.error(err);
  }
}

let notifyButton = document.getElementById("notify");
notifyButton.addEventListener("click", notify);