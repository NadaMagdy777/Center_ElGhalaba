

const selectImage = document.querySelector('.select-image');
const inputFile = document.querySelector('#file');
const imgArea = document.querySelector('.img-area');

selectImage.addEventListener('click', function () {
    inputFile.click();
})

inputFile.addEventListener('change', function () {
    const image = this.files[0]
    if (image.size < 2000000) {
        const reader = new FileReader();
        reader.onload = () => {
            const allImg = imgArea.querySelectorAll('img');
            allImg.forEach(item => item.remove());
            const imgUrl = reader.result;
            const img = document.createElement('img');
            img.src = imgUrl;
            imgArea.appendChild(img);
            imgArea.classList.add('active');
            imgArea.dataset.img = image.name;
        }
        reader.readAsDataURL(image);
    } else {
        alert("Image size more than 2MB");
    }
})


function selectedVeideo(self) {
    var file = self.files[0];
    var reader = new FileReader();

    reader.onload = function (e) {
        var src = e.target.result;
        var video = document.getElementById("video");
        var source = document.getElementById("source");

        source.setAttribute("src", src);
        video.load();
        video.play();
    };
    reader.readAsDataURL(file);
}


let file = document.querySelector('.upload');
let button = document.querySelector('btn2');
let progress = document.querySelector('progress');
let p_i = document.querySelector('.progress-indicator');
let load = 0;
let procces = "";

file.oninput = () => {
    let filename = file.files[0].name;
    let extension = filename.split('.').pop();
    let filesize = file.files[0].size;

    if (filesize <= 1000000) {
        filesize = (filesize / 1000).toFixed(2) + 'kb';
    }
    if (filesize == 1000000 || filesize <= 1000000000) {
        filesize = (filesize / 1000000).toFixed(2) + 'mb';
    }
    if (filesize == 1000000000 || filesize <= 1000000000000) {
        filesize = (filesize / 1000000000).toFixed(2) + 'gb';
    }

    document.querySelector('.lll').innerText = filename;
    document.querySelector('.ex').innerText = extension;
    document.querySelector('.size').innerText = filesize;

    getFile(filename);
}
let upload = () => {
    if (load >= 100) {
        clearInterval(procces);
        p_i.innerHTML = '100%' + ' ' + 'Upload Completed';
        button[0].classList.remove('active');
    }
    else {
        load++;
        progress.value = load;
        p_i.innerHTML = load + '%' + ' ' + 'Upload';
        button[1].onClick = e => {
            e.preventDefault();
            clearInterval(procces);
            document.querySelector('.pr').style.display = "none";
            button[1].style.visibility = 'hidden';
            button[0].classList.remove('active');

        }
    }
}
function getFile(filename) {
    if (filename) {
        document.querySelector('.pr').style.display = "block";
        load = 0;
        progress.value = 0;
        p_i.innerText = ' ';
        button[0].onClick = e => {
            e.preventDefault();
            button[0].classList.add('active');
            button[1].style.visibility = 'visible';
            procces = setInterval(upload, 100);
        }
    }
}
