<template>
  <div class="bg-[#f0f0f0] mt-5 ">
    <div class="wrapper flex !text-[#555] pb-12">
      <div class="col medium-4 small-12 large-4">
        <div class="col-inner">
          <h6>TRUNG TÂM BẢO HÀNH MIỀN BẮC</h6>
          <p class="dia-chi">
            Địa chỉ:26 Đường Trần Thái Tông,Quận Cầu Giấy, Hà Nội
          </p>
          <p class="phone">
            Tel: 0236 3611788 - 0236 3611688
          </p>
        </div>
      </div>
      <div class="col medium-4 small-12 large-4">
        <div class="col-inner">
          <h6>TRUNG TÂM BẢO HÀNH MIỀN TRUNG</h6>
          <p class="dia-chi">
            Địa Chỉ: số 10 Hoàng Thị Loan, Liên Chiều, Đà Nẵng
          </p>
          <p class="phone">
            Tel: 0236 3611788 - 0236 3611688
          </p>
        </div>
      </div>
      <div class="col medium-4 small-12 large-4">
        <div class="col-inner">
          <h6>TRUNG TÂM BẢO HÀNH MIỀN NAM</h6>
          <p class="dia-chi">
            Địa chỉ: 666/35 3/2 Đường 0, Thành phố Hồ Chí Minh
          </p>
          <p class="dia-chi">
            Hotline: 097.319.6066
          </p>
          <div class="social-icons follow-icons flex">
            <a
              href="https://www.facebook.com/akkovn"
              target="_blank"
              data-label="Facebook"
              rel="noopener noreferrer nofollow"
              class="icon primary button circle facebook tooltip tooltipstered"
            >
              <img
                src="/images/social_contact/Facebook_Logo_2023.png"
                alt=""
                class="w-8 h-8"
              >
            </a>
            <a
              href="https://www.instagram.com/akkogearvietnam/"
              target="_blank"
              rel="noopener noreferrer nofollow"
              data-label="Instagram"
              class="icon primary button circle instagram tooltip tooltipstered"
            >
              <img
                src="/images/social_contact/Instagram.png"
                alt=""
                class="w-8 h-8"
              >
            </a>
            <a
              href="mailto:lynotle183461@emailonacid.com"
              data-label="E-mail"
              rel="nofollow"
              class="icon primary button circle email tooltip tooltipstered"
            >
              <img
                src="/images/social_contact/Email.png"
                alt=""
                class="w-8 h-8"
              >
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="bg-black text-white p-2">
    <div class="">
      <div class="">
        <div class="">
          Copyright 2023 © <strong>LeKhanhToan</strong>.
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang='ts'>
import { inject, onMounted } from 'vue';
onMounted(() => {

})
</script>
<style scoped lang='scss'>
.wrapper {
  max-width: 1280px;
  margin-left: auto;
  margin-right: auto;
  padding-top: 24px;

  color: var(--main-color);
  /* @apply  */
}

.social-icons > a{
  @apply block pr-4 
}
</style>