<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ChatApplication.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
		<title>Chat App</title>
		<meta name="description" content="Latest updates and statistic charts">
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, shrink-to-fit=no">
        <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.6.16/webfont.js"></script>
		<script>
            WebFont.load({
                google: { "families": ["Poppins:300,400,500,600,700", "Roboto:300,400,500,600,700"] },
                active: function () {
                    sessionStorage.fonts = true;
                }
            });
        </script>
		<link href="assets/vendors/base/vendors.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/demo/default/base/style.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/vendors/custom/fullcalendar/fullcalendar.bundle.css" rel="stylesheet" type="text/css" />
		<link href="files/css/animate.css" rel="stylesheet" type="text/css" />
        <link rel="shortcut icon" href="img/logo.png" />
    <style>
        .m-widget3__text{
            padding-left:1rem;
        }
       
        
    </style>
</head>

<body class="m-page--fluid m--skin- m-content--skin-light2 m-header--fixed m-header--fixed-mobile m-aside-left--enabled m-aside-left--skin-dark m-aside-left--fixed m-aside-left--offcanvas m-footer--push m-aside--offcanvas-default">
		<div class="m-grid m-grid--hor m-grid--root m-page">

			<!-- BEGIN: Header -->
			<header id="m_header" class="m-grid__item    m-header " m-minimize-offset="200" m-minimize-mobile-offset="200">
				<div class="m-container m-container--fluid m-container--full-height">
					<div class="m-stack m-stack--ver m-stack--desktop">

						<!-- BEGIN: Brand -->
						<div class="m-stack__item m-brand  m-brand--skin-dark ">
							<div class="m-stack m-stack--ver m-stack--general">
								<div class="m-stack__item m-stack__item--middle m-brand__logo">
									<a href="index.html" class="m-brand__logo-wrapper">
										<img alt="" src="img/logo.png" />
									</a>
								</div>
								<div class="m-stack__item m-stack__item--middle m-brand__tools">

									<!-- BEGIN: Left Aside Minimize Toggle -->
									<a href="javascript:;" id="m_aside_left_minimize_toggle" class="m-brand__icon m-brand__toggler m-brand__toggler--left m--visible-desktop-inline-block  ">
										<span></span>
									</a>

									<!-- END -->

									<!-- BEGIN: Responsive Aside Left Menu Toggler -->
									<a href="javascript:;" id="m_aside_left_offcanvas_toggle" class="m-brand__icon m-brand__toggler m-brand__toggler--left m--visible-tablet-and-mobile-inline-block">
										<span></span>
									</a>

									<!-- END -->

									<!-- BEGIN: Responsive Header Menu Toggler -->
								

									<!-- END -->

									<!-- BEGIN: Topbar Toggler -->


									<!-- BEGIN: Topbar Toggler -->
								</div>
							</div>
						</div>

						<!-- END: Brand -->
						<div class="m-stack__item m-stack__item--fluid m-header-head" id="m_header_nav">

							<!-- BEGIN: Horizontal Menu -->
							<button class="m-aside-header-menu-mobile-close  m-aside-header-menu-mobile-close--skin-dark " id="m_aside_header_menu_mobile_close_btn"><i class="la la-close"></i></button>
							<div id="m_header_menu" class="m-header-menu m-aside-header-menu-mobile m-aside-header-menu-mobile--offcanvas  m-header-menu--skin-light m-header-menu--submenu-skin-light m-aside-header-menu-mobile--skin-dark m-aside-header-menu-mobile--submenu-skin-dark ">
								<ul class="m-menu__nav  m-menu__nav--submenu-arrow ">
									
								</ul>
							</div>

							<!-- END: Horizontal Menu -->

							<!-- BEGIN: Topbar -->
							<div id="m_header_topbar" class="m-topbar  m-stack m-stack--ver m-stack--general m-stack--fluid">
								<div class="m-stack__item m-topbar__nav-wrapper">
									<ul class="m-topbar__nav m-nav m-nav--inline">
										<div class="m-dropdown m-dropdown--inline m-dropdown--arrow" m-dropdown-toggle="click" aria-expanded="true">
													<a href = "" class="m-dropdown__toggle btn btn-primary dropdown-toggle myUsernameDiv" ></a>
													<div class="m-dropdown__wrapper" style="z-index: 101;">
														<span class="m-dropdown__arrow m-dropdown__arrow--left"></span>
														<div class="m-dropdown__inner">
															<div class="m-dropdown__body">
																<div class="m-dropdown__content">
																	<ul class="m-nav">
																		<li class="m-nav__section m-nav__section--first">
																			<span class="m-nav__section-text">Section</span>
																		</li>
																		<li class="m-nav__item">
																			<a href="" class="m-nav__link">+
																				<i class="m-nav__link-icon flaticon-share"></i>
																				<span class="m-nav__link-text">Activity</span>
																			</a>
																		</li>
																		<li class="m-nav__item">
																			<a href="" class="m-nav__link">
																				<i class="m-nav__link-icon flaticon-chat-1"></i>
																				<span class="m-nav__link-text">Messages</span>
																			</a>
																		</li>
																		<li class="m-nav__item">
																			<a href="" class="m-nav__link">
																				<i class="m-nav__link-icon flaticon-info"></i>
																				<span class="m-nav__link-text">FAQ</span>
																			</a>
																		</li>
																		<li class="m-nav__item">
																			<a href="" class="m-nav__link">
																				<i class="m-nav__link-icon flaticon-lifebuoy"></i>
																				<span class="m-nav__link-text">Support</span>
																			</a>
																		</li>
																		<li class="m-nav__separator m-nav__separator--fit">
																		</li>
																		<li class="m-nav__item  logout">
																			<a href="#" class="btn btn-outline-danger m-btn m-btn--pill m-btn--wide btn-sm logout">Logout</a>
																		</li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
												</div>
									</ul>
								</div>
							</div>
                            <!-- begin create group-->
                                    
                            <!-- end create group-->

							<!-- END: Topbar -->
						</div>
					</div>
				</div>
			</header>

			<!-- END: Header -->

			<!-- begin::Body -->
			<div class="m-grid__item m-grid__item--fluid m-grid m-grid--ver-desktop m-grid--desktop m-body">

				<!-- BEGIN: Left Aside -->
				<button class="m-aside-left-close  m-aside-left-close--skin-dark " id="m_aside_left_close_btn"><i class="la la-close"></i></button>
				<div id="m_aside_left" class="m-grid__item	m-aside-left  m-aside-left--skin-dark ">

					<!-- BEGIN: Aside Menu -->
					<div id="m_ver_menu" class="m-aside-menu  m-aside-menu--skin-dark m-aside-menu--submenu-skin-dark " m-menu-vertical="1" m-menu-scrollable="1" m-menu-dropdown-timeout="500" style="position: relative;">
						<ul class="m-menu__nav  m-menu__nav--dropdown-submenu-arrow " style="margin-bottom:0.1rem; padding-bottom:0px;">
							<li class="m-menu__item  m-menu__item--active" aria-haspopup="true"><a href="Default.aspx" class="m-menu__link "><i class="m-menu__link-icon flaticon-line-graph"></i><span class="m-menu__link-title"> <span class="m-menu__link-wrap"> <span class="m-menu__link-text">#ChatApp</span>
											</span></span></a></li>
							<li class="m-menu__section ">
								<h4 class="m-menu__section-text text-success">Online Users</h4>
								<i class="m-menu__section-icon flaticon-more-v2"></i>
							</li>
							
						</ul>
                        <ul class="m-menu__nav onlineUserListDiv  m-menu__nav--dropdown-submenu-arrow" style="margin-top:0.1rem; padding-top:0px;">
							
							
						</ul>
                        <hr />
                       
                        <ul class="m-menu__nav groupListDiv  m-menu__nav--dropdown-submenu-arrow" style="margin-top:0.1rem; padding-top:0px;">
							
							
						</ul>
                        <hr />
                        <ul class="m-menu__nav  m-menu__nav--dropdown-submenu-arrow " style="margin-bottom:0.1rem; padding-bottom:0px;">
                            <li class="m-menu__section ">
								<h4 class="m-menu__section-text text-success">Create Group</h4>
								<i class="m-menu__section-icon flaticon-more-v2"></i>
							</li>
							
							<li class="m-menu__section ">
								 <div class="m-form__group form-group">
                                     <input type="text" class="form-control createGroupName">
                                     <a href="javascript:void(0)" class="form-control createGroup btn-primary">Create</a>
                                 </div>
							</li>
							
						</ul>






					</div>
                    
					<!-- END: Aside Menu -->
				</div>

				<!-- END: Left Aside -->
				<div class="m-grid__item m-grid__item--fluid m-wrapper">

					<!-- BEGIN: Subheader -->
					<div class="m-subheader ">
						<div class="d-flex align-items-center">
							<div class="mr-auto">
								<h3 class="m-subheader__title ">#ChatApp</h3>
							</div>
						</div>
					</div>

					<!-- END: Subheader -->
					<div class="m-content">



						<!--Begin::Section-->
						<div class="row">
							<div class="col-xl-12">

								<!--begin:: Widgets/Support Tickets -->
								
								<div class="m-portlet m-portlet--full-height m-portlet--creative m-portlet--first m-portlet--bordered-semi" style="padding-bottom:0">
									<div class="m-portlet__head">
										<div class="m-portlet__head-caption">
											<div class="m-portlet__head-title">
												<span class="m-portlet__head-icon m--hide">
													<i class="flaticon-statistics"></i>
												</span>
												<h3 class="m-portlet__head-text">
													Sohbet
												</h3>
												<h2 class="m-portlet__head-label m-portlet__head-label--danger">
													<span class="chatName">#ChatApp</span>
												</h2>
											</div>
										</div>
									</div>
									<div class="m-portlet__body chatArea" data-scrollable="true" style="height: 525px">
										<div class="m-widget3">
											<div class="m-widget3__item">
												<div class="m-widget3__header">
													<div class="m-widget3__user-img">
														<img class="m-widget3__img" src="assets/app/media/img/users/user1.jpg" alt="">
													</div>
													<div class="m-widget3__info">
														<span class="m-widget3__username">
															Melania Trump
														</span><br>
														<span class="m-widget3__time">
															2 day ago
														</span>
													</div>
													<span class="m-widget3__status m--font-info">
														Delivered / Read / Undelivered
													</span>
												</div>
												<div class="m-widget3__body">
													
												</div>
											</div>
										</div>

									</div>
                                    <div class="input-group" style="height: 23px;box-shadow: 0 0 8px 0px #00000033;">
										<input type="text" class="form-control inputMessage" placeholder="..." aria-describedby="btnGroupAddon" />
										<div class="input-group-prepend"><button class="btn btn-primary"><span>GÖNDER</span></button></div>
									</div>
								</div>
								<!--end:: Widgets/Support Tickets -->
							</div>
						</div>

						<!--End::Section-->
					</div>
				</div>
			</div>

			<!-- end:: Body -->

			<!-- begin::Footer -->
			<footer class="m-grid__item		m-footer ">
				<div class="m-container m-container--fluid m-container--full-height m-page__container">
					<div class="m-stack m-stack--flex-tablet-and-mobile m-stack--ver m-stack--desktop">
						<div class="m-stack__item m-stack__item--left m-stack__item--middle m-stack__item--last">
							<span class="m-footer__copyright">
								2019 &copy; ChatApp by Ali Özgünay
							</span>
						</div>
						<div class="m-stack__item m-stack__item--right m-stack__item--middle m-stack__item--first">
							<ul class="m-footer__nav m-nav m-nav--inline m--pull-right">
								
							</ul>
						</div>
					</div>
				</div>
			</footer>

			<!-- end::Footer -->
		</div>


</body>
		<script src="assets/vendors/base/vendors.bundle.js" type="text/javascript"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
		<script src="assets/vendors/base/vendors.bundle.js" type="text/javascript"></script>
		<script src="assets/demo/default/base/scripts.bundle.js" type="text/javascript"></script>
		<script src="assets/vendors/custom/fullcalendar/fullcalendar.bundle.js" type="text/javascript"></script>
		<script src="assets/app/js/dashboard.js" type="text/javascript"></script>
		<script src="files/js/howler.min.js" type="text/javascript"></script>
        <script src="files/default.js"></script>
</html>
