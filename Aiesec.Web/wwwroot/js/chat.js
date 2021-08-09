const createRoomBtn = document.getElementById('createRoomBtn');
const createRoomModal = document.getElementById('createRoomModal');

createRoomBtn.addEventListener('click', function () {
    createRoomModal.classList.add('active');
});

function closeModal() {
    createRoomModal.classList.remove('active');
}